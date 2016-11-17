using FluentAssertions;
using FluentMetacritic.Domain;
using FluentMetacritic.Scraping;
using FluentMetacritic.UnitTests.Scraping.Builders;
using System;
using Xunit;

namespace FluentMetacritic.UnitTests.Scraping
{
    [Trait("Category", "UnitTest")]
    public class TrailerSearchResultScraperTests
    {
        [Fact]
        public void GivenThereIsATrailerSearchResultWithAllFieldsSet_WhenItIsScraped_AnEntityIsCreated()
        {
            var data = new Trailer("MARVEL'S THE AVENGERS")
                           {
                               ReleaseDate = new DateTime(2012, 5, 4),
                               Description = "Continuing the epic big-screen adventures started in Iron Man...",
                               Genres = new[] { "Action", "Adventure", "Sci-Fi", "Thriller" },
                               Rated = "PG-13",
                               Runtime = 143,
                               Starring = new[] { "Chris Evans", "Jeremy Renner", "Robert Downey Jr.", "Scarlett Johansson" },
                               UserScore = 7.9m,
                               MaturityRating = "E",
                               Publisher = "Zen Studios"
                           };

            var html = new TrailerSearchResultBuilder()
                .WithName(data.Name)
                .WithDescription(data.Description)
                .WithReleaseDate(data.ReleaseDate)
                .WithCast(data.Starring)
                .WithGenres(data.Genres)
                .WithRating(data.Rated)
                .WithRuntime(data.Runtime.Value)
                .WithUserScore(data.UserScore.Value)
                .WithMaturityRating(data.MaturityRating)
                .WithPublisher(data.Publisher)
                .Build();

            var scraper = new TrailerSearchResultScraper();

            var trailer = scraper.Scrape(html);

            trailer.ShouldBeEquivalentTo(data, options => options.IncludingAllDeclaredProperties());
        }

        [Fact]
        public void GivenThereIsATrailerSearchResultWithMinimumFieldsSet_WhenItIsScraped_AnEntityIsCreated()
        {
            var data = new Trailer("MARVEL'S THE AVENGERS");

            var html = new TrailerSearchResultBuilder()
                .WithName(data.Name)
                .Build();

            var scraper = new TrailerSearchResultScraper();

            var trailer = scraper.Scrape(html);

            trailer.ShouldBeEquivalentTo(data);
        }
    }
}