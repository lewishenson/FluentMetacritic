using FluentAssertions;
using FluentMetacritic.Domain;
using FluentMetacritic.Scraping;
using FluentMetacritic.UnitTests.Scraping.Builders;
using System;
using Xunit;

namespace FluentMetacritic.UnitTests.Scraping
{
    [Trait("Category", "UnitTest")]
    public class GameSearchResultScraperTests
    {
        [Fact]
        public void GivenThereIsAGameSearchResultWithAllFieldsSet_WhenItIsScraped_AnEntityIsCreated()
        {
            var data = new Game("LEGO Marvel Super Heroes", "PC", new DateTime(2013, 11, 15))
                           {
                               Description = "LEGO Marvel Super Heroes features an original story where players take control of Iron Man...",
                               Publisher = "Warner Bros. Interactive Entertainment",
                               MaturityRating = "E10+",
                               Score = 78
                           };

            var html = new GameSearchResultBuilder()
                .WithName(data.Name)
                .WithPlatform(data.Platform)
                .WithDescription(data.Description)
                .WithReleaseDate(data.ReleaseDate)
                .WithMaturityRating(data.MaturityRating)
                .WithPublisher(data.Publisher)
                .WithScore(data.Score.Value)
                .Build();

            var scraper = new GameSearchResultScraper();

            var game = scraper.Scrape(html);

            game.ShouldBeEquivalentTo(data);
        }

        [Fact]
        public void GivenThereIsAGameSearchResultWithMinimumFieldsSet_WhenItIsScraped_AnEntityIsCreated()
        {
            var data = new Game("A Game of Thrones", "PC", new DateTime(2012, 12, 31));

            var html = new GameSearchResultBuilder()
                .WithName(data.Name)
                .WithPlatform(data.Platform)
                .WithReleaseDate(data.ReleaseDate)
                .Build();

            var scraper = new GameSearchResultScraper();

            var game = scraper.Scrape(html);

            game.ShouldBeEquivalentTo(data);
        }
    }
}