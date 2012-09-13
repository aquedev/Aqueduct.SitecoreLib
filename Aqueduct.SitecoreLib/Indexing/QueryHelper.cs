using Lucene.Net.Search;

namespace Aqueduct.SitecoreLib.Indexing
{
	public class QueryHelper
	{
		public static Query MergeQueries(Query queryA, Query queryB, BooleanClause.Occur queryAOccurence, BooleanClause.Occur queryBOccurence)
		{
			BooleanQuery compoundQuery = new BooleanQuery();
			compoundQuery.Add(new BooleanClause(queryA, queryAOccurence));
			compoundQuery.Add(new BooleanClause(queryB, queryBOccurence));

			return compoundQuery;
		}

		public static Query MergeQueries(BooleanClause.Occur queryOccurence, params Query[] queries)
		{
			BooleanQuery compoundQuery = new BooleanQuery();
			foreach (Query query in queries)
			{
				compoundQuery.Add(new BooleanClause(query, queryOccurence));
			}
			return compoundQuery;
		}
	}
}