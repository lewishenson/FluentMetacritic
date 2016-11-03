using FluentMetacritic.DependencyInjection;
using FluentMetacritic.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentMetacritic.Query
{
    public static class IQueryDefinitionExtensions
    {
        public static async Task<IEnumerable<T>> ExecuteAsync<T>(this IQueryDefinition<T> queryDefinition) where T : IEntity
        {
            var queryExecutor = Factory.Instance.Create<IQueryExecutor<T>>();

            return await queryExecutor.ExecuteAsync(queryDefinition);
        }
    }
}