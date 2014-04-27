using FluentAssertions;
using FluentMetacritic.Domain;
using FluentMetacritic.Scraping;
using FluentMetacritic.UnitTests.Scraping.Builders;
using System;
using Xunit;

namespace FluentMetacritic.UnitTests.Scraping
{
    [Trait("Category", "UnitTest")]
    public class MovieSearchResultScraperTests
    {
        [Fact]
        public void GivenThereIsAMovieSearchResultWithAllFieldsSet_WhenItIsScraped_AnEntityIsCreated()
        {
            var data = new Movie("The Amazing Spider-Man", new DateTime(2012, 7, 3))
                           {
                               CriticScore = 66,
                               Description = "The Amazing Spider-Man is the story of Peter Parker...",
                               Genres = new[] { "Action", "Adventure", "Thriller", "Fantasy" },
                               Rated = "PG-13",
                               Runtime = 136,
                               Starring = new[] { "Andrew Garfield", "Emma Stone", "Irrfan Khan", "Martin Sheen", "Rhys Ifans", "Sally Field" },
                               UserScore = 7.2m
                           };

            var html = new MovieSearchResultBuilder()
                .WithName(data.Name)
                .WithDescription(data.Description)
                .WithReleaseDate(data.ReleaseDate)
                .WithCast(data.Starring)
                .WithCriticScore(data.CriticScore.Value)
                .WithGenres(data.Genres)
                .WithRating(data.Rated)
                .WithRuntime(data.Runtime.Value)
                .WithUserScore(data.UserScore.Value)
                .Build();

            var scraper = new MovieSearchResultScraper();

            var movie = scraper.Scrape(html);

            movie.ShouldBeEquivalentTo(data);
        }

        [Fact]
        public void GivenThereIsAMovieSearchResultWithMinimumFieldsSet_WhenItIsScraped_AnEntityIsCreated()
        {
            var data = new Movie("The Amazing Spider-Man 4", new DateTime(2018, 5, 4));

            var html = new MovieSearchResultBuilder()
                .WithName(data.Name)
                .WithReleaseDate(data.ReleaseDate)
                .Build();

            var scraper = new MovieSearchResultScraper();

            var movie = scraper.Scrape(html);

            movie.ShouldBeEquivalentTo(data);
        }
    }
}