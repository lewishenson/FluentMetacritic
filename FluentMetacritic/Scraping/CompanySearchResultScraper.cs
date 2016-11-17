using FluentMetacritic.Domain;
using HtmlAgilityPack;

namespace FluentMetacritic.Scraping
{
    public class CompanySearchResultScraper : SearchResultScraper<ICompany>
    {
        public override ICompany Scrape(HtmlNode node)
        {
            var name = ReadName(node);

            var company = new Company(name)
                           {
                               AverageCareerScore = ReadExtendedStat<int?>(node, "avg_career_score")
                           };

            return company;
        }
    }
}