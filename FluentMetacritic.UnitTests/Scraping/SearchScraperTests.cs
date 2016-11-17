using FluentAssertions;
using FluentMetacritic.Domain;
using FluentMetacritic.Scraping;
using System;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Extensions;

namespace FluentMetacritic.UnitTests.Scraping
{
    [Trait("Category", "UnitTest")]
    public class SearchScraperTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void GivenThereIsNoContent_WhenItIsScraped_AnEmptyCollectionIsReturned(string content)
        {
            var scraper = new SearchScraper();

            var entities = scraper.Scrape<IEntity>(content);

            entities.Should().BeEmpty();
        }

        [Fact]
        public void GivenThereAreNoSearchResultsInTheContent_WhenItIsScraped_AnEmptyCollectionIsReturned()
        {
            const string NoSearchResultsContent = "<html><head></head><body><div class=\"module search_results\"><div class=\"body\"><p>No search results found.</p></body></html>";

            var scraper = new SearchScraper();

            var entities = scraper.Scrape<IEntity>(NoSearchResultsContent);

            entities.Should().BeEmpty();
        }

        [Fact]
        public void GivenThereIsAnUnknownResultTypeInTheContent_WhenItIsScraped_AnExceptionIsThrown()
        {
            var scraper = new SearchScraper();

            var content = File.ReadAllText(@"Data\search_results.html");
            content = content.Replace("Feature", "Foo");

            Action scrapeAction = () => scraper.Scrape<IEntity>(content).ToList();

            scrapeAction.ShouldThrow<NotSupportedException>()
                        .WithMessage("Foo items are not supported.");
        }

        [Fact]
        public void GivenThereIsContentAndTheTypeIsEntity_WhenItIsScraped_AllEntitiesAreReturned()
        {
            var scraper = new SearchScraper();

            var content = File.ReadAllText(@"Data\search_results.html");
            var entities = scraper.Scrape<IEntity>(content);

            entities.Count().Should().Be(8);
        }

        [Fact]
        public void GivenThereIsContentAndTheTypeIsAlbum_WhenItIsScraped_TheAlbumIsReturned()
        {
            var expectedAlbum = new Album("The Glass Bead Game")
                                    {
                                        ReleaseDate = new DateTime(2009, 5, 26),
                                        Description = "The latest album for the British artist features singer Lavinia Blackwell, John Contreras on cello and Joolie Wood on violin.",
                                        Score = 82
                                    };

            var scraper = new SearchScraper();

            var content = File.ReadAllText(@"Data\search_results.html");
            var entities = scraper.Scrape<IAlbum>(content);

            entities.Count().Should().Be(1);

            var actualAlbum = entities.Single();
            actualAlbum.ShouldBeEquivalentTo(expectedAlbum);
        }

        [Fact]
        public void GivenThereIsContentAndTheTypeIsCompany_WhenItIsScraped_TheCompanyIsReturned()
        {
            var expectedCompany = new Company("Microsoft Game Studios");

            var scraper = new SearchScraper();

            var content = File.ReadAllText(@"Data\search_results.html");
            var entities = scraper.Scrape<ICompany>(content);

            entities.Count().Should().Be(1);

            var actualCompany = entities.Single();
            actualCompany.ShouldBeEquivalentTo(expectedCompany);
        }

        [Fact]
        public void GivenThereIsContentAndTheTypeIsFeature_WhenItIsScraped_TheFeatureIsReturned()
        {
            var expectedFeature = new Feature("Episode Review: Game of Thrones Season 2 Finale", "Jason Dietz", new DateTime(2012, 6, 4))
                                      {
                                          Description = "What are critics saying about \"Valar Morghulis,\" last night's second-season finale for HBO's hit Game of Thrones? Find out inside, where we've collected their comments about this week's episode and..."
                                      };

            var scraper = new SearchScraper();

            var content = File.ReadAllText(@"Data\search_results.html");
            var entities = scraper.Scrape<IFeature>(content);

            entities.Count().Should().Be(1);

            var actualFeature = entities.Single();
            actualFeature.ShouldBeEquivalentTo(expectedFeature);
        }

        [Fact]
        public void GivenThereIsContentAndTheTypeIsGame_WhenItIsScraped_TheGameIsReturned()
        {
            var expectedGame = new Game("Game of Thrones", "PC")
                                   {
                                       ReleaseDate = new DateTime(2012, 5, 15),
                                       Description = "Based on George R.R. Martin's best-selling 'A Song of Ice and Fire' series, Cyanide has developed games for the PC and next-gen consoles.",
                                       MaturityRating = "M",
                                       Publisher = "Atlus Co.",
                                       Score = 58
                                   };

            var scraper = new SearchScraper();

            var content = File.ReadAllText(@"Data\search_results.html");
            var entities = scraper.Scrape<IGame>(content);

            entities.Count().Should().Be(1);

            var actualGame = entities.Single();
            actualGame.ShouldBeEquivalentTo(expectedGame);
        }

        [Fact]
        public void GivenThereIsContentAndTheTypeIsMovie_WhenItIsScraped_TheMovieIsReturned()
        {
            var expectedMovie = new Movie("Ender's Game")
                                    {
                                        ReleaseDate = new DateTime(2013, 11, 1),
                                        CriticScore = 51,
                                        Description = "The Earth was ravaged twice by the Buggers, an alien race seemingly determined to destroy humanity. Seventy years later, the people of Earth remain banded together to prevent our own annihilation...",
                                        Genres = new[] { "Action", "Adventure", "Sci-Fi" },
                                        Rated = "PG-13",
                                        Runtime = 114,
                                        Starring = new[] { "Abigail Breslin", "Asa Butterfield", "Hailee Steinfeld", "Harrison Ford" },
                                        UserScore = 6.8m
                                    };

            var scraper = new SearchScraper();

            var content = File.ReadAllText(@"Data\search_results.html");
            var entities = scraper.Scrape<IMovie>(content);

            entities.Count().Should().Be(1);

            var actualMovie = entities.Single();
            actualMovie.ShouldBeEquivalentTo(expectedMovie);
        }

        [Fact]
        public void GivenThereIsContentAndTheTypeIsPerson_WhenItIsScraped_ThePersonIsReturned()
        {
            var expectedPerson = new Person("The Game")
                                     {
                                         AverageAlbumCareerScore = 70,
                                         AverageMovieCareerScore = 37,
                                         Categories = new[] { "Game", "Movie", "Album" },
                                         DateOfBirth = new DateTime(1941, 4, 3),
                                     };

            var scraper = new SearchScraper();

            var content = File.ReadAllText(@"Data\search_results.html");
            var entities = scraper.Scrape<IPerson>(content);

            entities.Count().Should().Be(1);

            var actualPerson = entities.Single();
            actualPerson.ShouldBeEquivalentTo(expectedPerson);
        }

        [Fact]
        public void GivenThereIsContentAndTheTypeIsTelevisionShow_WhenItIsScraped_TheTelevisionShowIsReturned()
        {
            var expectedTelevisionShow = new TelevisionShow("Star Trek: The Next Generation", new DateTime(1987, 9, 28))
                                             {
                                                 Description = "Space... The final frontier... These are the voyages of the Starship Enterprise. Its continuing mission: To explore strange new worlds... To seek out new life; new civilisations... To boldly go...",
                                                 Genres = new[] { "Drama", "Action & Adventure", "Science Fiction" },
                                                 Score = 51,
                                                 Starring = new[] { "Brent Spiner", "Colm Meaney", "Denise Crosby", "Diana Muldaur", "Gates McFadden", "Jonathan Frakes", "LeVar Burton", "Marina Sirtis", "Michael Dorn", "Patrick Stewart", "Wil Wheaton" }
                                             };

            var scraper = new SearchScraper();

            var content = File.ReadAllText(@"Data\search_results.html");
            var entities = scraper.Scrape<ITelevisionShow>(content);

            entities.Count().Should().Be(1);

            var actualTelevisionShow = entities.Single();
            actualTelevisionShow.ShouldBeEquivalentTo(expectedTelevisionShow);
        }

        [Fact]
        public void GivenThereIsContentAndTheTypeIsTrailer_WhenItIsScraped_TheTrailerIsReturned()
        {
            var expectedTrailer = new Trailer("The Wall - Game of Thrones Trailer")
                                      {
                                          ReleaseDate = new DateTime(2012, 5, 15),
                                          Description = "Based on George R.R. Martin's best-selling 'A Song of Ice and Fire' series, Cyanide has developed games for the PC and next-gen consoles.",
                                          MaturityRating = "M",
                                          Publisher = "Atlus Co."
                                      };

            var scraper = new SearchScraper();

            var content = File.ReadAllText(@"Data\search_results.html");
            var entities = scraper.Scrape<ITrailer>(content);

            entities.Count().Should().Be(1);

            var actualTrailer = entities.Single();
            actualTrailer.ShouldBeEquivalentTo(expectedTrailer);
        }
    }
}