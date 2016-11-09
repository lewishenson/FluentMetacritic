using FluentAssertions;
using FluentMetacritic.Domain;
using FluentMetacritic.Scraping;
using FluentMetacritic.UnitTests.Scraping.Builders;
using System;
using Xunit;

namespace FluentMetacritic.UnitTests.Scraping
{
    [Trait("Category", "UnitTest")]
    public class PersonSearchResultScraperTests
    {
        [Fact]
        public void GivenThereIsAnPersonSearchResultWithAllFieldsSet_WhenItIsScraped_AnEntityIsCreated()
        {
            var data = new Person("William Shatner")
                           {
                               AverageAlbumCareerScore = 52,
                               AverageMovieCareerScore = 68,
                               AverageTelevisionShowCareerScore = 69,
                               Categories = new[] { "Game", "TV Show", "Movie", "Album" },
                               DateOfBirth = new DateTime(1931, 3, 22)
                           };

            var html = new PersonSearchResultBuilder()
                .WithName(data.Name)
                .WithAverageAlbumCareerScore(data.AverageAlbumCareerScore.Value)
                .WithAverageMovieCareerScore(data.AverageMovieCareerScore.Value)
                .WithAverageTelevisionShowCareerScore(data.AverageTelevisionShowCareerScore.Value)
                .WithCategories(data.Categories)
                .WithReleaseDate(data.DateOfBirth.Value)
                .Build();

            var scraper = new PersonSearchResultScraper();

            var person = scraper.Scrape(html);

            person.ShouldBeEquivalentTo(data);
        }

        [Fact]
        public void GivenThereIsAnPersonSearchResultWithMinimumFieldsSet_WhenItIsScraped_AnEntityIsCreated()
        {
            var data = new Person("William Shatner");

            var html = new PersonSearchResultBuilder()
                .WithName(data.Name)
                .Build();

            var scraper = new PersonSearchResultScraper();

            var person = scraper.Scrape(html);

            person.ShouldBeEquivalentTo(data);
        }
    }
}