using FluentMetacritic.Domain;

namespace FluentMetacritic.Query
{
    public interface IPagingQueryBuilder<out T> where T : IEntity
    {
        IQueryBuilder<T> Page(int pageNumber);
    }
}