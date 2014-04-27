using FluentMetacritic.Domain;
using System.Collections.Generic;

namespace FluentMetacritic.Query
{
    public class QueryBuilder<T> : IQueryBuilder<T>, IOrderingQueryBuilder<T>, IPagingQueryBuilder<T> where T : IEntity
    {
        private readonly IQueryDefinition<T> _queryDefinition;

        public QueryBuilder(IQueryDefinition<T> queryDefinition)
        {
            _queryDefinition = queryDefinition;
        }

        public IOrderingQueryBuilder<T> OrderedBy()
        {
            return this;
        }

        public IPagingQueryBuilder<T> GoTo()
        {
            return this;
        }

        public IEnumerable<T> UsingText(string text)
        {
            _queryDefinition.Text = text;

            return _queryDefinition.Execute();
        }

        public IQueryBuilder<T> Relevancy()
        {
            _queryDefinition.OrderBy = OrderBy.Relevancy;

            return this;
        }

        public IQueryBuilder<T> Score()
        {
            _queryDefinition.OrderBy = OrderBy.Score;

            return this;
        }

        public IQueryBuilder<T> MostRecent()
        {
            _queryDefinition.OrderBy = OrderBy.MostRecent;

            return this;
        }

        public IQueryBuilder<T> Page(int pageNumber)
        {
            _queryDefinition.Page = pageNumber;

            return this;
        }
    }
}