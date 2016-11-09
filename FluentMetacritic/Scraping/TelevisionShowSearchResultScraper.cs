using FluentMetacritic.Domain;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace FluentMetacritic.Scraping
{
    public class TelevisionShowSearchResultScraper : SearchResultScraper<ITelevisionShow>
    {
        public override ITelevisionShow Scrape(HtmlNode node)
        {
            var name = ReadName(node);
            var releaseDate = ReadReleaseDate(node);

            var televisionShow = new TelevisionShow(name, releaseDate)
                           {
                               Description = ReadDescription(node),
                               Genres = ReadExtendedStats(node, "genre"),
                               Score = ReadValue<int?>(node, "./div[@class='result_wrap']/div/div[@class='main_stats']/span"),
                               Starring = ReadStarring(node)
                           };

            return televisionShow;
        }

        private IEnumerable<string> ReadStarring(HtmlNode node)
        {
            const string ActorPath = "./div[@class='result_wrap']/div/div[@class='basic_stat']/ul/li[@class='stat']/span[@class!='label cast']";
            const string StarringValue = "Starring:";

            var actorElements = node.SelectNodes(ActorPath);
            if (actorElements == null)
            {
                yield break;
            }

            foreach (var actorElement in actorElements)
            {
                var actorName = actorElement.InnerText.Trim(' ', ',');

                if (actorName.Equals(StarringValue, StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }

                yield return actorName;
            }
        }
    }
}