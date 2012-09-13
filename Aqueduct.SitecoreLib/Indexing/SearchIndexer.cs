using System;
using System.Collections.Generic;
using Lucene.Net.Documents;
using Sitecore.Data.Indexing;
using Sitecore.Data.Items;
using Aqueduct.Extensions;
using Aqueduct.Diagnostics;

namespace Aqueduct.SitecoreLib.Indexing
{
    public abstract class SearchIndexer : Index
    {
        private List<Field> _customFields;
        public static string DateTimeIndexingFormat = "{0:yyyyMMddHHmmss}";

        protected SearchIndexer(string name)
            : base(name)
        {
        }

        protected override void AddFields(Item item, Document document)
        {
            if (IsItemIndexable(item))
            {
                base.AddFields(item, document);

                _customFields = new List<Field>();
                try
                {
                    CreateCustomIndexFields(item);
                }
                catch (Exception ex)
                {
                    AppLogger.LogError(string.Format("Failed to create custom index fields for item {0}.", item.Paths.Path), ex);
                }

                foreach (Field customField in _customFields)
                    document.Add(customField);
            }
        }

        private bool IsItemIndexable(Item currentItem)
        {
            // if this is not a "__Standard Values" item then index
            const string standardValuesDefaultItemName = "__Standard Values";
            const string branchDefaultItemName = "$name";

            return currentItem.Name != standardValuesDefaultItemName && currentItem.Name != branchDefaultItemName &&
                   CanIndexItem(currentItem);
        }

        protected virtual bool CanIndexItem(Item currentItem) { return true; }
        protected virtual void CreateCustomIndexFields(Item currentItem) {}

        protected void AddGuidIndexField(string fieldName, Guid value)
        {
            string parsedValue = value.StripSpecialChars();

            _customFields.Add(CreateCustomIndexField(fieldName, parsedValue, false));
        }

        protected void AddTextIndexField(string fieldName, string value)
        {
            _customFields.Add(CreateCustomIndexField(fieldName, value, true));
        }

        protected void AddKeywordIndexField(string fieldName, string value)
        {
            _customFields.Add(CreateCustomIndexField(fieldName, value, false));
        }

        protected void AddDateTimeKeywordIndexField(string fieldName, DateTime value)
        {
            // convert datetime to format yyyyMMddTHHmmss (eg. 22 Apr 2009 15:36:00 becomes 20090422T153600)
            _customFields.Add(CreateCustomIndexField(fieldName, string.Format(DateTimeIndexingFormat, value), false));
        }

        private static Field CreateCustomIndexField(string fieldName, string value, bool tokenise)
        {
            // Store the original field value in the index.
            // http://incubator.apache.org/lucene.net/docs/2.1/Lucene.Net.Documents.Field.StoreFields.html
            Field.Store store = Field.Store.YES;

            Field.Index index;
            if (tokenise)
            {
                // Index the field's value using an Analyzer, so it can be searched.
                // http://incubator.apache.org/lucene.net/docs/2.1/Lucene.Net.Documents.Field.IndexMembers.html
                index = Field.Index.TOKENIZED;
            }
            else
            {
                // Index the field's value without using an Analyzer, so it can't be searched.
                // http://incubator.apache.org/lucene.net/docs/2.1/Lucene.Net.Documents.Field.IndexMembers.html
                index = Field.Index.UN_TOKENIZED;
            }

            return new Field(fieldName, value, store, index);
        }

        public static string[] GetTokenizedPath(Item item)
        {
            if (item == null)
                return new string[] { };

            string[] fragments = item.Paths.FullPath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            int i = 0;
            foreach (string fragment in fragments)
            {
                fragments[i] = string.Format("{0}/{1}", i + 1, fragment.ToLower().Replace(" ", ""));
                i++;
            }
            return fragments;
        }
    }
}