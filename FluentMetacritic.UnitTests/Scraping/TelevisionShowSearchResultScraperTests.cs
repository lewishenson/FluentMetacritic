using FluentAssertions;
using FluentMetacritic.Domain;
using FluentMetacritic.Scraping;
using FluentMetacritic.UnitTests.Scraping.Builders;
using System;
using Xunit;

namespace FluentMetacritic.UnitTests.Scraping
{
    [Trait("Category", "UnitTest")]
    public class TelevisionShowSearchResultScraperTests
    {
        [Fact]
        public void GivenThereIsATelevisionShowSearchResultWithAllFieldsSet_WhenItIsScraped_AnEntityIsCreated()
        {
            var data = new TelevisionShow("Firefly", new DateTime(2012, 9, 20))
                           {
                               Description = "Take my love, take my land, take me where I cannot stand...",
                               Genres = new[] { "Drama", "Suspense", "Science Fiction" },
                               Score = 63,
                               Starring = new[] { "Adam Baldwin", "Alan Tudyk", "Gina Torres", "Jewel Staite", "Morena Baccarin", "Nathan Fillion", "Ron Glass", "Sean Maher", "Summer Glau" },
                           };

            var html = new TelevisionShowSearchResultBuilder()
                .WithName(data.Name)
                .WithDescription(data.Description)
                .WithReleaseDate(data.StartDate)
                .WithCast(data.Starring)
                .WithGenres(data.Genres)
                .WithScore(data.Score.Value)
                .Build();

            var scraper = new TelevisionShowSearchResultScraper();

            var televisionShow = scraper.Scrape(html);

            televisionShow.ShouldBeEquivalentTo(data);
        }

        [Fact]
        public void GivenThereIsATelevisionShowSearchResultWithMinimumFieldsSet_WhenItIsScraped_AnEntityIsCreated()
        {
            var data = new TelevisionShow("Firefly", new DateTime(2012, 9, 20));

            var html = new TelevisionShowSearchResultBuilder()
                .WithName(data.Name)
                .WithReleaseDate(data.StartDate)
                .Build();

            var scraper = new TelevisionShowSearchResultScraper();

            var televisionShow = scraper.Scrape(html);

            televisionShow.ShouldBeEquivalentTo(data);
        }
    }
}