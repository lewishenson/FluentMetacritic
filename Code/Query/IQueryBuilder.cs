using FluentMetacritic.Domain;
using System.Collections.Generic;

namespace FluentMetacritic.Query
{
    public interface IQueryBuilder<out T>
        where T : IEntity
    {
        IOrderingQueryBuilder<T> OrderedBy();

        IPagingQueryBuilder<T> GoTo();

        IEnumerable<T> UsingText(string text);
    }
}