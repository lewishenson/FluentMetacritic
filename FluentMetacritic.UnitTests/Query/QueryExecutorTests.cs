using FluentAssertions;
using FluentMetacritic.Domain;
using FluentMetacritic.Net;
using FluentMetacritic.Query;
using FluentMetacritic.Scraping;

using NSubstitute;
using System;
using System.Net;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace FluentMetacritic.UnitTests.Query
{
    [Trait("Category", "UnitTest")]
    public class QueryExecutorTests
    {
        [Fact]
        public async void GivenTheMetacriticSiteIsUnavailable_WhenTheSearchIsExecuted_ThenAnExceptionWillBeThrown()
        {
            var webClient = Substitute.For<IHttpClient>();

            webClient.GetContentAsync(Arg.Any<string>())
                     .Throws(x => { throw new WebException(); });

            var searchScraper = Substitute.For<ISearchScraper>();

            var executor = new QueryExecutor<IEntity>(webClient, searchScraper);

            var queryDefinition = new QueryDefinition<IMovie> { Text = "sherlock" };

            var exception = await Assert.ThrowsAsync<MetacriticUnavailableException>(() => executor.ExecuteAsync(queryDefinition));

            exception.Message.Should().Be("Unable to perform search.");
            exception.InnerException.Should().BeOfType<WebException>();
        }

        [Fact]
        public async void GivenTheMetacriticSiteIsAvailable_WhenTheSearchIsExecuted_ThenTheEntitiesWillBeReturned()
        {
            var webClient = Substitute.For<IHttpClient>();

            var searchScraper = Substitute.For<ISearchScraper>();

            searchScraper.Scrape<IEntity>(Arg.Any<string>())
                         .Returns(new[] { new Company("Test") });

            var executor = new QueryExecutor<IEntity>(webClient, searchScraper);

            var queryDefinition = new QueryDefinition<IMovie> { Text = "sherlock" };

            var entities = await executor.ExecuteAsync(queryDefinition);

            entities.Should().NotBeEmpty();
        }
    }
}