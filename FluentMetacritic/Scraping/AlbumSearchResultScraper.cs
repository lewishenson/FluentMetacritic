using FluentMetacritic.Domain;
using HtmlAgilityPack;

namespace FluentMetacritic.Scraping
{
    public class AlbumSearchResultScraper : SearchResultScraper<IAlbum>
    {
        public override IAlbum Scrape(HtmlNode node)
        {
            var name = ReadName(node);

            var album = new Album(name)
            {
                ReleaseDate = ReadNullableReleaseDate(node),
                Description = ReadDescription(node),
                Score = ReadValue<int?>(node, "./div[@class='result_wrap']/div/div[@class='main_stats']/span")
            };

            return album;
        }
    }
}