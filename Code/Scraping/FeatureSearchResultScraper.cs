using FluentMetacritic.Domain;
using HtmlAgilityPack;

namespace FluentMetacritic.Scraping
{
    public class FeatureSearchResultScraper : SearchResultScraper<IFeature>
    {
        public override IFeature Scrape(HtmlNode node)
        {
            var name = ReadName(node);
            var postedBy = ReadExtendedStat<string>(node, "posted");
            var postedOn = ReadReleaseDate(node);

            var feature = new Feature(name, postedBy, postedOn)
                              {
                                  Description = ReadDescription(node)
                              };

            return feature;
        }
    }
}