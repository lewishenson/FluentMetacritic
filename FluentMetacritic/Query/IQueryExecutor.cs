using FluentMetacritic.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentMetacritic.Query
{
    public interface IQueryExecutor<T>
        where T : IEntity
    {
        Task<IEnumerable<T>> ExecuteAsync(IQueryDefinition<T> queryDefinition);
    }
}