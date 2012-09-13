//#undef DEBUG

using System;
using System.Collections.Generic;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Data.Templates;
using Sitecore.Diagnostics;
using Sitecore.Links;

namespace Aqueduct.SitecoreLib
{
    public static class SitecoreItemExtensions
    {
        public static string GetItemUrl(this Item item)
        {
            return LinkManager.GetItemUrl(item);
        }

        public static bool DescendsFromTemplate(this Item item, string templatePath)
        {
            if (!IsSet(item))
            {
                return false;
            }
            
            Template template = TemplateManager.GetTemplate(templatePath, item.Database);
            return template != null && item.DescendsFromTemplate(template);
        }

        public static bool DescendsFromTemplate(this Item item, Template template)
        {
            if (!IsSet(item, "While checking whether it DescendsFromTemplate"))
            {
                return false;
            }

            if (!IsSet(template, item.TemplateName))
            {
                return false;
            }

            Template itemTemplate = TemplateManager.GetTemplate(item);
            return itemTemplate != null && itemTemplate.DescendsFromOrEquals(template.ID);
        }

        public static bool HasAncestorWithTemplate(this Item item, string templatePath)
        {
            if (!IsSet(item))
            {
                return false;
            }

            return item.FindFirstAncestorWithTemplate(templatePath) != null;
        }


        public static string GetFieldValueInOrderOfPrecedence(this Item item, params string[] fieldNames)
        {
            foreach (string fieldName in fieldNames)
            {
                if (!string.IsNullOrEmpty(item[fieldName]))
                {
                    return item[fieldName];
                }
            }
            return string.Empty;
        }

        public static string GetFieldValueOrDefault(this Item item, string fieldName, string defaultValue)
        {
            return !string.IsNullOrEmpty(item[fieldName]) 
                ? item[fieldName] 
                : defaultValue;
        }

        public static Item FindFirstAncestorOrSelfWithTemplate(this Item item, string templatePath)
        {
            if (item.DescendsFromTemplate(templatePath))
            {
                return item;
            }
            return item.FindFirstAncestorWithTemplate(templatePath);
        }

        public static Item FindFirstAncestorWithTemplate(this Item item, string templatePath)
        {
            if (item.Parent == null)
                return null;

            if (item.Parent.DescendsFromTemplate(templatePath))
            {
                return item.Parent;
            }
            return FindFirstAncestorWithTemplate(item.Parent, templatePath);
        }

        public static T FindFirstAncestorWithTemplate<T>(this Item item, string templatePath, Converter<Item, T> converter)
        {
            Item firstAncestor = item.FindFirstAncestorWithTemplate(templatePath);

            if (firstAncestor == null) throw new Exception(string.Format(@"Could not find ancestor of ""{0}"" with template ""{1}""", item.Paths.Path, templatePath));

            return converter(firstAncestor);
        }

        public static T FindFirstChildInheritingTemplate<T>(this Item item, string templatePath, Converter<Item, T> converter)
        {
            foreach (Item child in item.Children)
            {
                if (child.DescendsFromTemplate(templatePath))
                {
                    return converter(child);
                }
            }
            return default(T);
        }

        public static List<T> GetChildrenInheritingFromTemplate<T>(this Item item, string templatePath, Converter<Item, T> converter)
        {
            var results = new List<T>();
            foreach (Item child in item.Children)
            {
                if (child.DescendsFromTemplate(templatePath))
                {
                    results.Add(converter(child));
                }
            }
            return results;
        }

        public static List<T> GetDescendantsInheritingFromTemplate<T>(this Item item, string templatePath, Converter<Item, T> converter)
        {
            Template template = TemplateManager.GetTemplate(templatePath, item.Database);
            var results = new List<T>();
            foreach (Item descendant in item.Axes.GetDescendants())
            {
                if (descendant.DescendsFromTemplate(template))
                {
                    results.Add(converter(descendant));
                }
            }
            return results;
        }

        public static DateTime GetDateTime(this Item item, string fieldName)
        {
            string value = item[fieldName];
            DateTime defaultValue = DateTime.MinValue;
            DateTime result = DateUtil.ParseDateTime(value, defaultValue);
            if (result == defaultValue)
            {
                throw new Exception(String.Format("Invalid value for field '{0}' (Item Path: {1})", fieldName, item.Paths.Path));
            }
            return result;
        }

        public static bool IsTemplateItem(this Item item)
        {
            if (!IsSet(item))
            {
                return false;
            }

            return item.TemplateName.ToLower() == "template";
        }

        private static bool IsSet<T>(T t, string message = "")
            where T: class
        {
            bool result = true;
#if DEBUG
            Assert.IsNotNull(t, String.Format ("{0} should not be null. Message: {1}", typeof (T).Name, message));
#else
            result = (t != null);
            if (result == false)
                Aqueduct.Diagnostics.AppLogger.LogWarningMessage(String.Format("{0} should not be null. Message: {1}", typeof(T).Name, message));    
#endif

            return result;

        }
    }
}