using FluentMetacritic.Domain;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentMetacritic.Scraping
{
    public class SearchScraper : ISearchScraper
    {
        private static readonly IDictionary<string, ISearchResultScraper<IEntity>> Scrapers = new Dictionary<string, ISearchResultScraper<IEntity>>(StringComparer.InvariantCultureIgnoreCase);

        static SearchScraper()
        {
            Scrapers.Add("Album", new AlbumSearchResultScraper());
            Scrapers.Add("Company", new CompanySearchResultScraper());
            Scrapers.Add("Feature", new FeatureSearchResultScraper());
            Scrapers.Add("Game", new GameSearchResultScraper());
            Scrapers.Add("Movie", new MovieSearchResultScraper());
            Scrapers.Add("People", new PersonSearchResultScraper());
            Scrapers.Add("TV Show", new TelevisionShowSearchResultScraper());
            Scrapers.Add("Trailer", new TrailerSearchResultScraper());
        }

        public IEnumerable<T> Scrape<T>(string content) where T : IEntity
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return Enumerable.Empty<T>();
            }

            var document = new HtmlDocument();
            document.LoadHtml(content);

            var searchResults = document.DocumentNode.SelectNodes("//*[@id=\"main\"]/div[@class=\"module search_results\"]/div/ul/li");
            var allEntities = ScrapeSearchResults(searchResults);

            return allEntities.OfType<T>();
        }

        private IEnumerable<IEntity> ScrapeSearchResults(IEnumerable<HtmlNode> searchResults)
        {
            foreach (var searchResult in searchResults)
            {
                var resultType = GetResultType(searchResult);
                if (string.IsNullOrWhiteSpace(resultType))
                {
                    continue;
                }

                var scraper = GetResultScraper(resultType);
                var entity = scraper.Scrape(searchResult);

                yield return entity;
            }
        }

        private string GetResultType(HtmlNode searchResult)
        {
            var selectSingleNode = searchResult.SelectSingleNode("./div[@class=\"result_type\"]/strong");

            return selectSingleNode != null ? selectSingleNode.InnerText : null;
        }

        private ISearchResultScraper<IEntity> GetResultScraper(string resultType)
        {
            if (!Scrapers.ContainsKey(resultType))
            {
                var exceptionMessage = string.Format("{0} items are not supported.", resultType);
                throw new NotSupportedException(exceptionMessage);
            }

            return Scrapers[resultType];
        }
    }
}