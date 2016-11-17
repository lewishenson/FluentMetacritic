using FluentMetacritic.Domain;
using HtmlAgilityPack;

namespace FluentMetacritic.Scraping
{
    public class GameSearchResultScraper : SearchResultScraper<IGame>
    {
        public override IGame Scrape(HtmlNode node)
        {
            var name = ReadName(node);
            var platform = ReadValue<string>(node, "./div[@class='result_type']/span[@class='platform']");

            var game = new Game(name, platform)
                           {
                               ReleaseDate = ReadNullableReleaseDate(node),
                               Description = ReadDescription(node),
                               Publisher = ReadExtendedStat<string>(node, "publisher"),
                               MaturityRating = ReadExtendedStat<string>(node, "maturity_rating"),
                               Score = ReadValue<int?>(node, "./div[@class='result_wrap']/div/div[@class='main_stats']/span")
                           };

            return game;
        }

        protected override string GenerateExtendedStatPath(string extendedStatClass)
        {
            return $"./div[@class='result_wrap']/div/div[@class='more_stats extended_stats']/ul/li[@class='stat {extendedStatClass}']/span[@class!='label']";
        }
    }
}