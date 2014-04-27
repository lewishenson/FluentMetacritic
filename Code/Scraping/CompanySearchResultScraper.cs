using FluentMetacritic.Domain;
using HtmlAgilityPack;
using System;

namespace FluentMetacritic.Scraping
{
    public class CompanySearchResultScraper : SearchResultScraper<ICompany>
    {
        public override ICompany Scrape(HtmlNode node)
        {
            var name = ReadName(node);

            var company = new Company(name)
                           {
                               AverageCareerScore = ReadAverageCareerScore(node)
                           };

            return company;
        }

        private int? ReadAverageCareerScore(HtmlNode node)
        {
            const string ToBeDecidedScore = "tbd";

            var rawAverageCareerScore = ReadExtendedStat<string>(node, "avg_career_score");

            if (string.IsNullOrWhiteSpace(rawAverageCareerScore) || rawAverageCareerScore.Equals(ToBeDecidedScore, StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }

            return Converter.SafeConvert<int>(rawAverageCareerScore);
        }
    }
}