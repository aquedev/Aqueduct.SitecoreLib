using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Indexing;
using Sitecore.Data.Items;

namespace Aqueduct.SitecoreLib.Indexing
{
    [Obsolete("Use Search Query Context instead")]
    public class QueryContext<TFieldNames> where TFieldNames : IndexFieldNames
	{
		private static Database m_database;
        
		#region Properties

		public Query QueryDefinition { get; set; }
		public Sort SortDefinition { get; set; }
		public Index Index { get; set; }
		public bool IsUsingDefaultQueryDefinition { get; set; }

		protected static Database CurrentDatabase
		{
			get
			{
				if (Context.Database == null)
					return m_database;

				if (Context.Database.Name == DatabaseNames.Core)
					return Context.ContentDatabase;

				return Context.Database;
			}
		}

		#endregion

		public QueryContext(Database database)
			: this()
		{
			m_database = database;
		}

		public QueryContext()
		{
			IsUsingDefaultQueryDefinition = true;
		}

		public QueryContext<TFieldNames> Where(Expression<Func<TFieldNames, string>> searchFilterExpression, Guid value)
		{
			string searchFieldName = ((ConstantExpression) (searchFilterExpression.Body)).Value.ToString();
			string parsedValue = value.ToString("N");

			var queryDefinition = new TermQuery(new Term(searchFieldName, parsedValue));
			MergeQueryDefinitions(queryDefinition);

			return this;
		}

		public QueryContext<TFieldNames> Where(Expression<Func<TFieldNames, string>> searchFilterExpression, string value)
		{
			string searchFieldName = ((ConstantExpression) (searchFilterExpression.Body)).Value.ToString();

			var queryDefinition = new TermQuery(new Term(searchFieldName, value));
			MergeQueryDefinitions(queryDefinition);

			return this;
		}

        public QueryContext<TFieldNames> LessThan(Expression<Func<TFieldNames, string>> searchFilterExpression, DateTime date)
        {
            string searchFieldName = ((ConstantExpression)(searchFilterExpression.Body)).Value.ToString();
//            RangeQuery queryDefinition = new RangeQuery(new Term(searchFieldName, string.Format(SearchIndexer.DateTimeIndexingFormat, DateTime.MinValue)), new Term(searchFieldName, string.Format(SearchIndexer.DateTimeIndexingFormat, date)), true);
            var queryDefinition = new ConstantScoreRangeQuery(searchFieldName, string.Format(SearchIndexer.DateTimeIndexingFormat, DateTime.MinValue), string.Format(SearchIndexer.DateTimeIndexingFormat, date), true, true);
            IsUsingDefaultQueryDefinition = false;
            MergeQueryDefinitions(queryDefinition);

            return this;
        }

        public QueryContext<TFieldNames> GreaterThan(Expression<Func<TFieldNames, string>> searchFilterExpression, DateTime date)
        {
            string searchFieldName = ((ConstantExpression)(searchFilterExpression.Body)).Value.ToString();
//            RangeQuery queryDefinition = new RangeQuery(new Term(searchFieldName, string.Format(SearchIndexer.DateTimeIndexingFormat, date)), new Term(searchFieldName, string.Format(SearchIndexer.DateTimeIndexingFormat, DateTime.MaxValue)), true);
            var queryDefinition = new ConstantScoreRangeQuery(searchFieldName, string.Format(SearchIndexer.DateTimeIndexingFormat, date), string.Format(SearchIndexer.DateTimeIndexingFormat, DateTime.MaxValue), true, true);
            IsUsingDefaultQueryDefinition = false;
            MergeQueryDefinitions(queryDefinition);

            return this;
        }

        public QueryContext<TFieldNames> Between(Expression<Func<TFieldNames, string>> searchFilterExpression, DateTime startDate, DateTime endDate)
        {
            string searchFieldName = ((ConstantExpression)(searchFilterExpression.Body)).Value.ToString();

            var queryDefinition = new RangeQuery(new Term(searchFieldName, string.Format(SearchIndexer.DateTimeIndexingFormat, startDate)), new Term(searchFieldName, string.Format(SearchIndexer.DateTimeIndexingFormat, endDate)), true);
            IsUsingDefaultQueryDefinition = false;
            MergeQueryDefinitions(queryDefinition);

            return this;
        }

		public QueryContext<TFieldNames> OrderBy(Expression<Func<TFieldNames, string>> sortFieldExpression, SortOrder sortOrder, ComparisonType comparison)
		{
			string sortFieldName = ((ConstantExpression) (sortFieldExpression.Body)).Value.ToString();
			bool order = (sortOrder != SortOrder.Ascending);
			int comparer = (comparison == ComparisonType.String ? SortField.STRING : SortField.LONG);

			SortDefinition = new Sort(new SortField(sortFieldName, comparer, order));

			return this;
		}

		public QueryContext<TFieldNames> OrderByRelevance()
		{
			SortDefinition = Sort.RELEVANCE;

			return this;
		}

		public T GetFirstResult<T>(Converter<Item, T> convert)
		{
			return AsSearchResult(convert, 0, 1).FirstOrDefault();
		}

		public List<T> AsSearchResult<T>(Converter<Item, T> convert)
		{
			return AsSearchResult(convert, 0, 0);
		}

		public List<T> AsSearchResult<T>(Converter<Item, T> convert, int pageIndex, int pageSize)
		{
			IndexSearcher indexSearcher = Index.GetSearcher(CurrentDatabase);
            
			// search against the index. apply sort if necessary
			Hits results = SortDefinition == null
			               	? indexSearcher.Search(QueryDefinition)
			               	: indexSearcher.Search(QueryDefinition, SortDefinition);
			
			int totalResults = results.Length();

			// if pagesize not specified - get all items
			if (pageSize == 0)
				pageSize = totalResults;

			// Convert hits to document list
			var documents = new List<Document>();
			int firstPageItemIndex = (pageIndex*pageSize);
			int lastPageItemIndex = firstPageItemIndex + pageSize;

			for (int i = 0; i < totalResults; i++)
			{
				if (i >= firstPageItemIndex && i < lastPageItemIndex)
				{
					documents.Add(results.Doc(i));
				}
			}

			// Conver document list to Sitecore item list
			var result = new List<T>(totalResults);

			foreach (Document document in documents)
			{
				string itemId = document.GetField(LuceneDocumentFieldNames.Id).StringValue();
				Item item = CurrentDatabase.GetItem(itemId);

				if (item != null)
					result.Add(convert(item));
			}

			return result;
		}

		public List<T> AsSearchResult<T>(Converter<Document, T> convert)
		{
			IndexSearcher indexSearcher = Index.GetSearcher(CurrentDatabase);

			// search against the index. apply sort if necessary
			Hits results = SortDefinition == null
							? indexSearcher.Search(QueryDefinition)
							: indexSearcher.Search(QueryDefinition, SortDefinition);

			int totalResults = results.Length();

			var result = new List<T>(totalResults);

			for (int i = 0; i < totalResults; i++)
			{
				Document document = results.Doc(i);
				result.Add(convert(document));
			}

			return result;
		}

		private void MergeQueryDefinitions(Query newQueryDefinition)
		{
		    // if current query definition is the one created by index searcher - replace; otherwise - merge
		    QueryDefinition = IsUsingDefaultQueryDefinition 
                ? newQueryDefinition 
                : QueryHelper.MergeQueries(QueryDefinition, newQueryDefinition, BooleanClause.Occur.MUST, BooleanClause.Occur.MUST);

		    IsUsingDefaultQueryDefinition = false;
		}

	    #region Nested type: LuceneDocumentFieldNames

		private static class LuceneDocumentFieldNames
		{
			public const string Id = "Id";
		}

		#endregion
	}
}