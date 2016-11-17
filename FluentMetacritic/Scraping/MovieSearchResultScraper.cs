using FluentMetacritic.Domain;
using HtmlAgilityPack;

namespace FluentMetacritic.Scraping
{
    public class MovieSearchResultScraper : SearchResultScraper<IMovie>
    {
        public override IMovie Scrape(HtmlNode node)
        {
            var name = ReadName(node);

            var movie = new Movie(name)
                            {
                                ReleaseDate = ReadNullableReleaseDate(node),
                                CriticScore = ReadValue<int?>(node, "./div[@class='result_wrap']/div/div[@class='main_stats']/span"),
                                Description = ReadDescription(node),
                                Genres = ReadExtendedStats(node, "genre"),
                                Rated = ReadExtendedStat<string>(node, "rating"),
                                Runtime = ReadRunTime(node),
                                Starring = ReadExtendedStats(node, "cast"),
                                UserScore = ReadExtendedStat<decimal?>(node, "product_avguserscore")
                            };

            return movie;
        }

        private int? ReadRunTime(HtmlNode node)
        {
            var rawRunTime = ReadExtendedStat<string>(node, "runtime");
            if (string.IsNullOrWhiteSpace(rawRunTime))
            {
                return null;
            }

            return Converter.SafeConvert<int?>(rawRunTime.Replace(" min", string.Empty));
        }

        protected override string GenerateExtendedStatPath(string extendedStatClass)
        {
            return $"./div[@class='result_wrap']/div/div[@class='more_stats extended_stats']/ul/li[@class='stat {extendedStatClass}']/span[@class!='label']";
        }
    }
}