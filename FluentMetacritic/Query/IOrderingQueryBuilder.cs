using FluentMetacritic.Domain;

namespace FluentMetacritic.Query
{
    public interface IOrderingQueryBuilder<T> where T : IEntity
    {
        IQueryBuilder<T> Relevancy();

        IQueryBuilder<T> Score();

        IQueryBuilder<T> MostRecent();
    }
}