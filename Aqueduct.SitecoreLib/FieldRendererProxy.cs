using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Aqueduct.SitecoreLib.DataAccess;
using Sitecore.Web.UI.WebControls;
using Aqueduct.Domain;
using Sitecore.Data.Items;
using Sitecore.Data;
using System.Reflection;

namespace Aqueduct.SitecoreLib
{
    public class FieldRendererProxy<TDomainEntity> where TDomainEntity : ISitecoreDomainEntity
    {
        private readonly TDomainEntity m_domainEntity;

        public FieldRendererProxy(TDomainEntity domainEntity)
        {
            m_domainEntity = domainEntity;
        }

		public string RenderFromDomainEntityInNormalMode<TProperty>(Expression<Func<TDomainEntity, TProperty>> expression)
		{
			if (Sitecore.Context.PageMode.IsNormal)
			{
				PropertyInfo propertyInfo = GetPropertyInfo(expression);
				return propertyInfo.GetValue(m_domainEntity, null).ToString();
			}
			
            return Render(expression);
		}

        public string Render<TProperty>(Expression<Func<TDomainEntity, TProperty>> expression)
        {
            return FieldRenderer.Render(GetItem(), GetFieldName(expression));
        }

        public string Render<TProperty>(Expression<Func<TDomainEntity, TProperty>> expression, FieldRendererArgs args)
        {
            var renderer = new FieldRenderer
                               {
                                   Item = GetItem (),
                                   FieldName = GetFieldName (expression),
                                   Parameters = String.Join ("&", GetParameters (args))
                               };

            // maybe i'm losing it but if you want line breaks on multi-line text fields then you to add 'linebreaks'
            // to both Parameters and RenderParameters
            if (!string.IsNullOrEmpty(args.LineBreaks))
            {
                renderer.RenderParameters.Add("linebreaks", args.LineBreaks);
                
            }            
            return renderer.Render ();
        }

        private static string[] GetParameters(FieldRendererArgs args)
        {
            var list = new List<string>();
            
            AddRendererParameter(list, "format", args.Format);
			
            if (args.Width != default(int))
			{
			    AddRendererParameter(list, "mw", args.Width.ToString());
			}
			
            if (args.Height != default(int))
			{
			    AddRendererParameter(list, "mh", args.Height.ToString());
			}
			
            AddRendererParameter(list, "text", args.LinkText);
			
            if (args.HideCopyrightMessage)
			{
			    AddRendererParameter(list, "hideCopyright", args.HideCopyrightMessage ? "1" : "0");
			}
            
            if (!string.IsNullOrEmpty(args.LineBreaks))
            {
                AddRendererParameter(list, "linebreaks", args.LineBreaks);
            }

            if (!string.IsNullOrEmpty(args.CssClass))
            {
                AddRendererParameter(list, "class", args.CssClass);
            } 
            return list.ToArray ();
        }

        private static void AddRendererParameter(ICollection<string> list, string name, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                list.Add(string.Format("{0}={1}", name, value));
            }
        }

        private Item GetItem()
        {
            return Databases.CurrentDatabase.GetItem(new ID(m_domainEntity.Id));
        }

        private static string GetFieldName<TPropertyType>(Expression<Func<TDomainEntity, TPropertyType>> expression)
        {
            PropertyInfo propertyInfo = GetPropertyInfo(expression);

        	Map<TDomainEntity> map = MapFinder.FindMap<TDomainEntity>();

            MapEntry mapEntry = map.Mappings.Single(x => x.MappedProperty.Name == propertyInfo.Name);

            return mapEntry.MappedTo;
        }

    	private static PropertyInfo GetPropertyInfo<TPropertyType>(Expression<Func<TDomainEntity, TPropertyType>> expression)
    	{
    		var propertyInfo = ((MemberExpression)expression.Body).Member as PropertyInfo;
    		if (propertyInfo == null)
    		{
    			throw new ArgumentException("The lambda expression 'property' should point to a valid Property");
    		}
    		return propertyInfo;
    	}
    }
}