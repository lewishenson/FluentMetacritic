using FluentMetacritic.Domain;

using HtmlAgilityPack;

namespace FluentMetacritic.Scraping
{
    public class TrailerSearchResultScraper : SearchResultScraper<ITrailer>
    {
        public override ITrailer Scrape(HtmlNode node)
        {
            var name = ReadName(node);

            var trailer = new Trailer(name)
                              {
                                  ReleaseDate = ReadNullableReleaseDate(node),
                                  Description = ReadDescription(node),
                                  Genres = ReadExtendedStats(node, "genre"),
                                  Rated = ReadExtendedStat<string>(node, "rating"),
                                  Runtime = ReadRunTime(node),
                                  Starring = ReadExtendedStats(node, "cast"),
                                  UserScore = ReadExtendedStat<decimal?>(node, "product_avguserscore"),
                                  Publisher = ReadExtendedStat<string>(node, "publisher"),
                                  MaturityRating = ReadExtendedStat<string>(node, "maturity_rating"),
                              };

            return trailer;
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