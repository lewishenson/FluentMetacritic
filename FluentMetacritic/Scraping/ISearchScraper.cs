using FluentMetacritic.Domain;
using System.Collections.Generic;

namespace FluentMetacritic.Scraping
{
    public interface ISearchScraper
    {
        IEnumerable<T> Scrape<T>(string content) where T : IEntity;
    }
}