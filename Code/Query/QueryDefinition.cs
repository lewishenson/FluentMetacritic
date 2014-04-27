using FluentMetacritic.Domain;
using System.Diagnostics;

namespace FluentMetacritic.Query
{
    [DebuggerDisplay("Text = {Text}")]
    public class QueryDefinition<T> : IQueryDefinition<T>
        where T : IEntity
    {
        public QueryDefinition()
        {
            Category = Category.FromEntity<T>();
            OrderBy = OrderBy.Relevancy;
        }

        public Category Category { get; set; }

        public OrderBy OrderBy { get; set; }

        public int? Page { get; set; }

        public string Text { get; set; }
    }
}