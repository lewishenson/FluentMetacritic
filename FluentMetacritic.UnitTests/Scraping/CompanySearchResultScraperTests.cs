using FluentAssertions;
using FluentMetacritic.Domain;
using FluentMetacritic.Scraping;
using FluentMetacritic.UnitTests.Scraping.Builders;
using Xunit;

namespace FluentMetacritic.UnitTests.Scraping
{
    [Trait("Category", "UnitTest")]
    public class CompanySearchResultScraperTests
    {
        [Fact]
        public void GivenThereIsAnCompanySearchResultWithAllFieldsSet_WhenItIsScraped_AnEntityIsCreated()
        {
            var data = new Company("Microsoft")
                           {
                               AverageCareerScore = 73
                           };

            var html = new CompanySearchResultBuilder()
                .WithName(data.Name)
                .WithAverageCareerScore(data.AverageCareerScore.Value)
                .Build();

            var scraper = new CompanySearchResultScraper();

            var company = scraper.Scrape(html);

            company.ShouldBeEquivalentTo(data);
        }

        [Fact]
        public void GivenThereIsAnCompanySearchResultWithMinimumFieldsSet_WhenItIsScraped_AnEntityIsCreated()
        {
            var data = new Company("Microsoft Research");

            var html = new CompanySearchResultBuilder()
                .WithName(data.Name)
                .Build();

            var scraper = new CompanySearchResultScraper();

            var company = scraper.Scrape(html);

            company.ShouldBeEquivalentTo(data);
        }
    }
}