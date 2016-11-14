using FluentAssertions;
using FluentMetacritic.Domain;
using FluentMetacritic.Net;
using FluentMetacritic.Query;
using FluentMetacritic.Scraping;

using System;
using System.Net;
using Moq;
using Xunit;

namespace FluentMetacritic.UnitTests.Query
{
    [Trait("Category", "UnitTest")]
    public class QueryExecutorTests
    {
        [Fact]
        public async void GivenTheMetacriticSiteIsUnavailable_WhenTheSearchIsExecuted_ThenAnExceptionWillBeThrown()
        {
            var httpClient = Mock.Of<IHttpClient>();
            Mock.Get(httpClient)
                .Setup(c => c.GetContentAsync(It.IsAny<string>()))
                .Throws<WebException>();

            var searchScraper = Mock.Of<ISearchScraper>();

            var executor = new QueryExecutor<IEntity>(httpClient, searchScraper);

            var queryDefinition = new QueryDefinition<IMovie> { Text = "sherlock" };

            var exception = await Assert.ThrowsAsync<MetacriticUnavailableException>(() => executor.ExecuteAsync(queryDefinition));

            exception.Message.Should().Be("Unable to perform search.");
            exception.InnerException.Should().BeOfType<WebException>();
        }

        [Fact]
        public async void GivenTheMetacriticSiteIsAvailable_WhenTheSearchIsExecuted_ThenTheEntitiesWillBeReturned()
        {
            var httpClient = Mock.Of<IHttpClient>();

            var searchScraper = Mock.Of<ISearchScraper>();
            Mock.Get(searchScraper)
                .Setup(s => s.Scrape<IEntity>(It.IsAny<string>()))
                .Returns(new[] { new Company("Test") });

            var executor = new QueryExecutor<IEntity>(httpClient, searchScraper);

            var queryDefinition = new QueryDefinition<IMovie> { Text = "sherlock" };

            var entities = await executor.ExecuteAsync(queryDefinition);

            entities.Should().NotBeEmpty();
        }
    }
}