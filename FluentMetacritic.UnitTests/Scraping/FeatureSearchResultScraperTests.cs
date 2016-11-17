using FluentAssertions;
using FluentMetacritic.Domain;
using FluentMetacritic.Scraping;
using FluentMetacritic.UnitTests.Scraping.Builders;
using System;
using Xunit;

namespace FluentMetacritic.UnitTests.Scraping
{
    [Trait("Category", "UnitTest")]
    public class FeatureSearchResultScraperTests
    {
        [Fact]
        public void GivenThereIsAnFeatureSearchResultWithAllFieldsSet_WhenItIsScraped_AnEntityIsCreated()
        {
            var data = new Feature("Fall TV 2nd Look: Marvel's Agents of S.H.I.E.L.D.", "Jason Dietz", new DateTime(2013, 11, 14))
                           {
                               Description = "After a promising start..."
                           };

            var html = new FeatureSearchResultBuilder()
                .WithName(data.Name)
                .WithDescription(data.Description)
                .WithPostedBy(data.PostedBy)
                .WithReleaseDate(data.PostedOn)
                .Build();

            var scraper = new FeatureSearchResultScraper();

            var feature = scraper.Scrape(html);

            feature.ShouldBeEquivalentTo(data);
        }
    }
}