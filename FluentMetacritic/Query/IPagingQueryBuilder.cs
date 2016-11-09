using FluentMetacritic.Domain;

namespace FluentMetacritic.Query
{
    public interface IPagingQueryBuilder<T> where T : IEntity
    {
        IQueryBuilder<T> Page(int pageNumber);
    }
}