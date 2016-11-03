using FluentMetacritic.Domain;
using FluentMetacritic.Net;
using FluentMetacritic.Query;
using FluentMetacritic.Scraping;
using FluentMetacritic.Search;
using System;
using System.Collections.Generic;

namespace FluentMetacritic.DependencyInjection
{
    public class Factory : IFactory
    {
        private static readonly IFactory TheInstance;

        private readonly IDictionary<Type, Func<object>> _actions = new Dictionary<Type, Func<object>>();

        static Factory()
        {
            TheInstance = new Factory();
        }

        private Factory()
        {
            Initialise();
        }

        public static IFactory Instance
        {
            get
            {
                return TheInstance;
            }
        }

        public T Create<T>()
        {
            var key = typeof(T);
            if (_actions.ContainsKey(key))
            {
                return (T)_actions[key]();
            }

            var exceptionMessage = string.Format("Unable to create {0} instance.", key.FullName);
            throw new InvalidOperationException(exceptionMessage);
        }

        private void Initialise()
        {
            AddAction<IMetacriticSearch>(() => new MetacriticSearch());

            AddAction<IQueryBuilder<IEntity>>(() => new QueryBuilder<IEntity>(Instance.Create<IQueryDefinition<IEntity>>()));
            AddAction<IQueryBuilder<IAlbum>>(() => new QueryBuilder<IAlbum>(Instance.Create<IQueryDefinition<IAlbum>>()));
            AddAction<IQueryBuilder<ICompany>>(() => new QueryBuilder<ICompany>(Instance.Create<IQueryDefinition<ICompany>>()));
            AddAction<IQueryBuilder<IGame>>(() => new QueryBuilder<IGame>(Instance.Create<IQueryDefinition<IGame>>()));
            AddAction<IQueryBuilder<IMovie>>(() => new QueryBuilder<IMovie>(Instance.Create<IQueryDefinition<IMovie>>()));
            AddAction<IQueryBuilder<IPerson>>(() => new QueryBuilder<IPerson>(Instance.Create<IQueryDefinition<IPerson>>()));
            AddAction<IQueryBuilder<ITrailer>>(() => new QueryBuilder<ITrailer>(Instance.Create<IQueryDefinition<ITrailer>>()));
            AddAction<IQueryBuilder<ITelevisionShow>>(() => new QueryBuilder<ITelevisionShow>(Instance.Create<IQueryDefinition<ITelevisionShow>>()));

            AddAction<IQueryDefinition<IEntity>>(() => new QueryDefinition<IEntity>());
            AddAction<IQueryDefinition<IAlbum>>(() => new QueryDefinition<IAlbum>());
            AddAction<IQueryDefinition<ICompany>>(() => new QueryDefinition<ICompany>());
            AddAction<IQueryDefinition<IGame>>(() => new QueryDefinition<IGame>());
            AddAction<IQueryDefinition<IMovie>>(() => new QueryDefinition<IMovie>());
            AddAction<IQueryDefinition<IPerson>>(() => new QueryDefinition<IPerson>());
            AddAction<IQueryDefinition<ITrailer>>(() => new QueryDefinition<ITrailer>());
            AddAction<IQueryDefinition<ITelevisionShow>>(() => new QueryDefinition<ITelevisionShow>());

            AddAction<IQueryExecutor<IEntity>>(() => new QueryExecutor<IEntity>(Instance.Create<IHttpClient>(), Instance.Create<ISearchScraper>()));
            AddAction<IQueryExecutor<IAlbum>>(() => new QueryExecutor<IAlbum>(Instance.Create<IHttpClient>(), Instance.Create<ISearchScraper>()));
            AddAction<IQueryExecutor<ICompany>>(() => new QueryExecutor<ICompany>(Instance.Create<IHttpClient>(), Instance.Create<ISearchScraper>()));
            AddAction<IQueryExecutor<IGame>>(() => new QueryExecutor<IGame>(Instance.Create<IHttpClient>(), Instance.Create<ISearchScraper>()));
            AddAction<IQueryExecutor<IMovie>>(() => new QueryExecutor<IMovie>(Instance.Create<IHttpClient>(), Instance.Create<ISearchScraper>()));
            AddAction<IQueryExecutor<IPerson>>(() => new QueryExecutor<IPerson>(Instance.Create<IHttpClient>(), Instance.Create<ISearchScraper>()));
            AddAction<IQueryExecutor<ITrailer>>(() => new QueryExecutor<ITrailer>(Instance.Create<IHttpClient>(), Instance.Create<ISearchScraper>()));
            AddAction<IQueryExecutor<ITelevisionShow>>(() => new QueryExecutor<ITelevisionShow>(Instance.Create<IHttpClient>(), Instance.Create<ISearchScraper>()));

            AddAction<ISearchScraper>(() => new SearchScraper());

            AddAction<IHttpClient>(() => new HttpClientWrapper());
        }

        private void AddAction<T>(Func<object> value)
        {
            var key = typeof(T);

            _actions.Add(key, value);
        }
    }
}