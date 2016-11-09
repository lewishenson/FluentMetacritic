using FluentMetacritic.Domain;
using HtmlAgilityPack;
using System;

namespace FluentMetacritic.Scraping
{
    public class PersonSearchResultScraper : SearchResultScraper<IPerson>
    {
        public override IPerson Scrape(HtmlNode node)
        {
            var name = ReadName(node);

            var person = new Person(name)
                           {
                               AverageAlbumCareerScore = ReadAverageCareerScore(node, "Album"),
                               AverageMovieCareerScore = ReadAverageCareerScore(node, "Movie"),
                               AverageTelevisionShowCareerScore = ReadAverageCareerScore(node, "TV"),
                               Categories = ReadExtendedStats(node, "categories"),
                               DateOfBirth = ReadDateOfBirth(node)
                           };

            return person;
        }

        private int? ReadAverageCareerScore(HtmlNode node, string careerType)
        {
            const string AverageCareerScorePath = "./div[@class='result_wrap']/div/div[@class='basic_stat']/ul/li[@class='stat avg_career_score']";

            var averageCareerScoreElements = node.SelectNodes(AverageCareerScorePath);
            if (averageCareerScoreElements == null)
            {
                return null;
            }

            foreach (var averageCareerScoreElement in averageCareerScoreElements)
            {
                var label = averageCareerScoreElement.SelectSingleNode("span[@class='label']");
                if ((label == null) || (label.InnerText.IndexOf(careerType, StringComparison.InvariantCultureIgnoreCase) < 0))
                {
                    continue;
                }

                var data = averageCareerScoreElement.SelectSingleNode("span[@class!='label']");
                if (data == null)
                {
                    continue;
                }

                return Converter.SafeConvert<int?>(data.InnerText);
            }

            return null;
        }

        protected virtual DateTime? ReadDateOfBirth(HtmlNode node)
        {
            var path = GenerateExtendedStatPath("release_date");
            return ReadValue<DateTime?>(node, path);
        }
    }
}