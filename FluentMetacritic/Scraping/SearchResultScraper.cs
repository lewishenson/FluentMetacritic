using FluentMetacritic.Domain;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentMetacritic.Scraping
{
    public abstract class SearchResultScraper<TEntity> : ISearchResultScraper<TEntity>
        where TEntity : IEntity
    {
        public abstract TEntity Scrape(HtmlNode node);

        private string ReadValue(HtmlNode node, string path)
        {
            var result = node.SelectSingleNode(path);

            return result == null ? null : HtmlEntity.DeEntitize(result.InnerText.Trim());
        }

        protected virtual T ReadValue<T>(HtmlNode node, string path)
        {
            var elementValue = ReadValue(node, path);

            return Converter.SafeConvert<T>(elementValue);
        }

        protected virtual string ReadName(HtmlNode node)
        {
            return ReadValue(node, "./div[@class='result_wrap']/div/div[@class='main_stats']/h3/a");
        }

        protected virtual string ReadDescription(HtmlNode node)
        {
            return ReadValue(node, "./div[@class='result_wrap']/p[@class='deck basic_stat']/following-sibling::text()");
        }

        protected virtual DateTime ReadReleaseDate(HtmlNode node)
        {
            var path = GenerateExtendedStatPath("release_date");
            return ReadValue<DateTime>(node, path);
        }

        protected virtual T ReadExtendedStat<T>(HtmlNode node, string extendedStatClass)
        {
            const string ToBeDecidedScore = "tbd";

            var path = GenerateExtendedStatPath(extendedStatClass);
            var rawValue = ReadValue(node, path);

            if (string.IsNullOrWhiteSpace(rawValue) || rawValue.Equals(ToBeDecidedScore, StringComparison.OrdinalIgnoreCase))
            {
                return default(T);
            }

            return Converter.SafeConvert<T>(rawValue);
        }

        protected virtual string GenerateExtendedStatPath(string extendedStatClass)
        {
            return string.Format("./div[@class='result_wrap']/div/div[@class='basic_stat']/ul/li[@class='stat {0}']/span[@class!='label']", extendedStatClass);
        }

        protected virtual IEnumerable<string> ReadExtendedStats(HtmlNode node, string extendedStatClass)
        {
            var rawValue = ReadExtendedStat<string>(node, extendedStatClass);
            if (string.IsNullOrWhiteSpace(rawValue))
            {
                return Enumerable.Empty<string>();
            }

            return rawValue.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                           .Select(s => HtmlEntity.DeEntitize(s.Trim()));
        }
    }
}