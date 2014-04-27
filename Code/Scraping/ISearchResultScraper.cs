using HtmlAgilityPack;

namespace FluentMetacritic.Scraping
{
    public interface ISearchResultScraper<out T>
    {
        T Scrape(HtmlNode node);
    }
}