using FluentMetacritic.Domain;

using System.Collections.Generic;

namespace FluentMetacritic.Query
{
    public interface IQueryExecutor<T>
        where T : IEntity
    {
        IEnumerable<T> Execute(IQueryDefinition<T> queryDefinition);
    }
}