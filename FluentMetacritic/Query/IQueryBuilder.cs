using FluentMetacritic.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluentMetacritic.Query
{
    public interface IQueryBuilder<T>
        where T : IEntity
    {
        IOrderingQueryBuilder<T> OrderedBy();

        IPagingQueryBuilder<T> GoTo();

        Task<IEnumerable<T>> UsingTextAsync(string text);
    }
}