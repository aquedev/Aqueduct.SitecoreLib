using System;
using System.Collections;
using System.IO;
using System.Threading;

namespace Aqueduct.SitecoreLib.Packaging
{
	public class PackageInfo
	{
		#region Variables

		private DateTime? m_StartTime;
		private DateTime? m_EndTime;

		private string m_Name;
		private string m_FileBasePath;
		private string m_SitecoreBasePath;

		private string m_BasePath;
		private string m_XmlPath;
		private string m_PackagePath;
		
		private string m_Database;


		private bool m_BuildPackage;

		private DateTime m_BaseDate = DateTime.Now;

		private ArrayList m_Items;
		private ArrayList m_Files;
		
		private object m_AddItemLockingObject = new object();
		private object m_AddFileLockingObject = new object();

		#endregion

		public PackageInfo(string name, string outputPath)
		{
			m_Name = name;
			m_StartTime = DateTime.Now;
			m_BasePath = outputPath;
			m_XmlPath = Path.Combine(outputPath, "XML");
			m_PackagePath = Path.Combine(outputPath, "Builds");
			
			m_Items = new ArrayList();
			m_Files = new ArrayList();
		}
		
		public void AddItem(string path)
		{
			AddToListWithLocking(m_Items, path, m_AddItemLockingObject);
		}
		
		public void AddFile(string path)
		{
			AddToListWithLocking(m_Files, path, m_AddFileLockingObject);
		}
		
		private void AddToListWithLocking(ArrayList list, object value, object lockingObject)
		{
			if (Monitor.TryEnter(lockingObject, TimeSpan.FromSeconds(2.0d)))
			{
				try
				{
					list.Add(value);
				}
				catch
				{

				}
				finally
				{
					Monitor.Exit(lockingObject);
				}
			}
			else
			{
				throw new TimeoutException("AddToListWithLocking timed out waiting for lock to release.");
			}
		}

		#region Accessors

		public DateTime? StartTime
		{
			get { return m_StartTime; }
			set { m_StartTime = value; }
		}

		public DateTime? EndTime
		{
			get { return m_EndTime; }
			set { m_EndTime = value; }
		}

		public string Database
		{
			get { return m_Database; }
			set { m_Database = value; }
		}

		public bool BuildPackage
		{
			get { return m_BuildPackage; }
			set { m_BuildPackage = value; }
		}

		public int FileCount
		{
			get
			{
				return m_Files.Count;
			}
		}

		public int ItemCount
		{
			get
			{
				return m_Items.Count;
			}
		}

		public string[] GetItems()
		{
			return m_Items.ToArray(typeof(string)) as string[];
		}

		public string[] GetFiles()
		{
			return m_Files.ToArray(typeof(string)) as string[];
		}

		public string Name
		{
			get { return m_Name; }
			set { m_Name = value; }
		}

		public string FileBasePath
		{
			get { return m_FileBasePath; }
			set { m_FileBasePath = value; }
		}

		public string SitecoreBasePath
		{
			get { return m_SitecoreBasePath; }
			set { m_SitecoreBasePath = value; }
		}

		public DateTime BaseDate
		{
			get { return m_BaseDate; }
			set { m_BaseDate = value; }
		}

		public string XmlPath
		{
			get { return m_XmlPath; }
			set { m_XmlPath = value; }
		}

		public string PackagePath
		{
			get { return m_PackagePath; }
			set { m_PackagePath = value; }
		}

		#endregion

	}
}
