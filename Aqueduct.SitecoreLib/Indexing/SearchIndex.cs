using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;

namespace Aqueduct.SitecoreLib.Indexing
{
	
	public abstract class SearchIndex<TFieldNames> where TFieldNames : IndexFieldNames
	{
		private static Database m_database;

		protected SearchIndex()
		{
		}

		protected SearchIndex(string databaseName)
		{
			m_database = Factory.GetDatabase(databaseName);
		}

        protected SearchIndex(Database database)
        {
            m_database = database;
        }

		protected static Database CurrentDatabase
		{
			get
			{
				if (m_database != null)
					return m_database;

				return Context.Database;
			}
		}

		protected QueryContext<TFieldNames> GetQueryContext()
		{
			return new QueryContext<TFieldNames>(CurrentDatabase);
		}

		public abstract QueryContext<TFieldNames> GetAllItems();
		public abstract QueryContext<TFieldNames> SearchAllItems(string queryText);
	}
}