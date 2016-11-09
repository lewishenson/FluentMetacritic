using FluentMetacritic.Domain;
using HtmlAgilityPack;

namespace FluentMetacritic.Scraping
{
    public class AlbumSearchResultScraper : SearchResultScraper<IAlbum>
    {
        public override IAlbum Scrape(HtmlNode node)
        {
            var name = ReadName(node);
            var releaseDate = ReadReleaseDate(node);

            var album = new Album(name, releaseDate)
            {
                Description = ReadDescription(node),
                Score = ReadValue<int?>(node, "./div[@class='result_wrap']/div/div[@class='main_stats']/span")
            };

            return album;
        }
    }
}