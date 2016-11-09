using FluentMetacritic.Domain;
using FluentMetacritic.Query;

namespace FluentMetacritic.Search
{
    public interface IMetacriticSearch
    {
        IQueryBuilder<IEntity> AllItems();

        IQueryBuilder<IAlbum> Albums();

        IQueryBuilder<ICompany> Companies();

        IQueryBuilder<IGame> Games();

        IQueryBuilder<IMovie> Movies();

        IQueryBuilder<IPerson> People();

        IQueryBuilder<ITrailer> Trailers();

        IQueryBuilder<ITelevisionShow> TelevisionShows();
    }
}