using FluentMetacritic.DependencyInjection;
using FluentMetacritic.Domain;
using System.Collections.Generic;

namespace FluentMetacritic.Query
{
    public static class IQueryDefinitionExtensions
    {
        public static IEnumerable<T> Execute<T>(this IQueryDefinition<T> queryDefinition) where T : IEntity
        {
            var queryExecutor = Factory.Instance.Create<IQueryExecutor<T>>();

            return queryExecutor.Execute(queryDefinition);
        }
    }
}