using FluentMetacritic.Domain;
using FluentMetacritic.Net;
using FluentMetacritic.Scraping;
using System.Collections.Generic;
using System.Net;

namespace FluentMetacritic.Query
{
    public class QueryExecutor<T> : IQueryExecutor<T>
        where T : IEntity
    {
        private readonly IWebClient _webClient;

        private readonly ISearchScraper _searchScraper;

        public QueryExecutor(IWebClient webClient, ISearchScraper searchScraper)
        {
            _webClient = webClient;
            _searchScraper = searchScraper;
        }

        public IEnumerable<T> Execute(IQueryDefinition<T> queryDefinition)
        {
            var content = GetSearchPageContent(queryDefinition);
            var entities = _searchScraper.Scrape<T>(content);

            return entities;
        }

        private string GetSearchPageContent(IQueryDefinition<T> queryDefinition)
        {
            var uri = GetSearchPageUri(queryDefinition);

            try
            {
                return _webClient.GetContent(uri);
            }
            catch (WebException ex)
            {
                throw new MetacriticUnavailableException("Unable to perform search.", ex);
            }
        }

        private string GetSearchPageUri(IQueryDefinition<T> queryDefinition)
        {
            return UriGenerator.Create()
                               .ForCategory(queryDefinition.Category)
                               .OrderedBy(queryDefinition.OrderBy)
                               .Page(queryDefinition.Page)
                               .Generate(queryDefinition.Text);
        }
    }
}