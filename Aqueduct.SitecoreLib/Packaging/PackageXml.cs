using System.Collections;
using System.Text;
using System.Xml;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace Aqueduct.SitecoreLib.Packaging
{
	public class PackageXml
	{
		private Hashtable m_Databases;

		private bool m_IncludeAllSitecoreItemVersions = false;

		public bool IncludeAllSitecoreItemVersions
		{
			get { return m_IncludeAllSitecoreItemVersions; }
			set { m_IncludeAllSitecoreItemVersions = value; }
		}

		public XmlDocument GetXmlFromPackage(PackageInfo package)
		{
			m_Databases = new Hashtable();

			StringBuilder sb = new StringBuilder();

			sb.Append("<project>");
			sb.Append("<Metadata>");
			sb.Append("<metadata>");
			sb.Append("<PackageName>Package Generator package</PackageName>");
			sb.Append("<Author>ItemPackager.Data.Classes.PackageXml</Author>");
			sb.Append("<Version />");
			sb.AppendFormat("<Revision>{0}</Revision>", package.StartTime.Value.ToString("yyyy-MM-dd hh:mm:ss"));
			sb.Append("<License />");
			sb.AppendFormat("<Comment>Automatic package build for all changes after {0}</Comment>",
							package.BaseDate.ToString("yyyy-MM-dd hh:mm:ss"));
			sb.Append("<Attributes />");
			sb.Append("<Readme>This package was auto-generated. Please use at own risk.</Readme>");
			sb.Append("<Publisher />");
			sb.Append("<PostStep />");
			sb.Append("<PackageID />");
			sb.Append("</metadata>");
			sb.Append("</Metadata>");

			sb.Append("<Sources>");

			if (package.ItemCount > 0)
			{
				sb.Append("<xitems>");

				sb.Append("<Converter>");
				sb.Append("<ItemToEntryConverter>");
				sb.Append("<Transforms>");
				sb.Append("<InstallerConfigurationTransform>");
				sb.Append("<Options>");
				sb.Append("<BehaviourOptions>");
				sb.Append("<ItemMode>Merge</ItemMode>");
				sb.Append("<ItemMergeMode>Append</ItemMergeMode>");
				sb.Append("</BehaviourOptions>");
				sb.Append("</Options>");
				sb.Append("</InstallerConfigurationTransform>");
				sb.Append("</Transforms>");
				sb.Append("</ItemToEntryConverter>");
				sb.Append("</Converter>");

				sb.Append("<Entries>");

				string[] items = package.GetItems();

				foreach (string item in items)
				{
					sb.AppendFormat("<x-item>/{0}{1}/invariant/{2}</x-item>", package.Database,
									GetPackageItemPath(package.Database, item), GetVersion(package.Database, item));
				}

				sb.Append("</Entries>");
				sb.AppendFormat("<SkipVersions>{0}</SkipVersions>", m_IncludeAllSitecoreItemVersions.ToString());
				sb.Append("<Name>Items</Name>");
				sb.Append("</xitems>");
			}

			if (package.FileCount > 0)
			{
				sb.Append("<xfiles>");
				sb.Append("<Entries>");

				string[] files = package.GetFiles();

				foreach (string file in files)
				{
					sb.AppendFormat("<x-item>{0}</x-item>", file);
				}

				sb.Append("</Entries>");
				sb.Append("<Converter>");
				sb.Append("<FileToEntryConverter>");
				sb.Append("<Root>/</Root>");
				sb.Append("<Transforms />");
				sb.Append("</FileToEntryConverter>");
				sb.Append("</Converter>");
				sb.Append("<Include />");
				sb.Append("<Exclude />");
				sb.Append("<Name>Files</Name>");
				sb.Append("</xfiles>");
			}

			sb.Append("</Sources>");

			sb.Append("<Converter>");
			sb.Append("<TrivialConverter>");
			sb.Append("<Transforms />");
			sb.Append("</TrivialConverter>");
			sb.Append("</Converter>");
			sb.Append("<Include /><Exclude />");
			sb.Append("<Name />");
			sb.Append("</project>");

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(sb.ToString());

			return xmlDoc;
		}

		private string GetPackageItemPath(string database, string path)
		{
			Database db = GetDatabase(database);

			Item sitecoreItem = db.SelectSingleItem(path);

			return string.Format("{0}/{1}", sitecoreItem.Paths.FullPath, sitecoreItem.ID.ToString());
		}

		private string GetVersion(string database, string path)
		{
			if (m_IncludeAllSitecoreItemVersions)
				return "0";

			Database db = GetDatabase(database);

			Item sitecoreItem = db.SelectSingleItem(path);

			return sitecoreItem.Version.ToString();
		}

		private Database GetDatabase(string database)
		{
			if (m_Databases.ContainsKey(database))
				return m_Databases[database] as Database;

			Database db = Factory.GetDatabase(database);

			m_Databases.Add(database, db);

			return db;
		}
	}
}