using FluentMetacritic.DependencyInjection;
using FluentMetacritic.Domain;
using FluentMetacritic.Query;

namespace FluentMetacritic.Search
{
    public class MetacriticSearch : IMetacriticSearch
    {
        public IQueryBuilder<IEntity> AllItems()
        {
            return CreateQueryBuilder<IEntity>();
        }

        public IQueryBuilder<IAlbum> Albums()
        {
            return CreateQueryBuilder<IAlbum>();
        }

        public IQueryBuilder<ICompany> Companies()
        {
            return CreateQueryBuilder<ICompany>();
        }

        public IQueryBuilder<IGame> Games()
        {
            return CreateQueryBuilder<IGame>();
        }

        public IQueryBuilder<IMovie> Movies()
        {
            return CreateQueryBuilder<IMovie>();
        }

        public IQueryBuilder<IPerson> People()
        {
            return CreateQueryBuilder<IPerson>();
        }

        public IQueryBuilder<ITrailer> Trailers()
        {
            return CreateQueryBuilder<ITrailer>();
        }

        public IQueryBuilder<ITelevisionShow> TelevisionShows()
        {
            return CreateQueryBuilder<ITelevisionShow>();
        }

        private IQueryBuilder<T> CreateQueryBuilder<T>() where T : IEntity
        {
            return Factory.Instance.Create<IQueryBuilder<T>>();
        }
    }
}