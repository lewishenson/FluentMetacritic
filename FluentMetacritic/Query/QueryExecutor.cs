using System;
using FluentMetacritic.Domain;
using FluentMetacritic.Net;
using FluentMetacritic.Scraping;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FluentMetacritic.Query
{
    public class QueryExecutor<T> : IQueryExecutor<T>
        where T : IEntity
    {
        private readonly IHttpClient _httpClient;

        private readonly ISearchScraper _searchScraper;

        public QueryExecutor(IHttpClient httpClient, ISearchScraper searchScraper)
        {
            _httpClient = httpClient;
            _searchScraper = searchScraper;
        }

        public async Task<IEnumerable<T>> ExecuteAsync(IQueryDefinition<T> queryDefinition)
        {
            var content = await GetSearchPageContent(queryDefinition);
            var entities = _searchScraper.Scrape<T>(content);

            return entities;
        }

        private async Task<string> GetSearchPageContent(IQueryDefinition<T> queryDefinition)
        {
            var uri = GetSearchPageUri(queryDefinition);

            try
            {
                return await _httpClient.GetContentAsync(uri);
            }
            catch (Exception ex)
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