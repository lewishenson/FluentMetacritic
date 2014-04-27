using FluentAssertions;
using FluentMetacritic.Domain;
using FluentMetacritic.Net;
using FluentMetacritic.Query;
using FluentMetacritic.Scraping;

using NSubstitute;
using System;
using System.Net;
using Xunit;

namespace FluentMetacritic.UnitTests.Query
{
    [Trait("Category", "UnitTest")]
    public class QueryExecutorTests
    {
        [Fact]
        public void GivenTheMetacriticSiteIsUnavailable_WhenTheSearchIsExecuted_ThenAnExceptionWillBeThrown()
        {
            var webClient = Substitute.For<IWebClient>();

            webClient.GetContent(Arg.Any<string>())
                     .Returns(x => { throw new WebException(); });

            var searchScraper = Substitute.For<ISearchScraper>();

            var executor = new QueryExecutor<IEntity>(webClient, searchScraper);

            var queryDefinition = new QueryDefinition<IMovie> { Text = "sherlock" };

            Action executeAction = () => executor.Execute(queryDefinition);

            executeAction.ShouldThrow<MetacriticUnavailableException>()
                         .WithInnerException<WebException>()
                         .WithMessage("Unable to perform search.");
        }

        [Fact]
        public void GivenTheMetacriticSiteIsAvailable_WhenTheSearchIsExecuted_ThenTheEntitiesWillBeReturned()
        {
            var webClient = Substitute.For<IWebClient>();

            var searchScraper = Substitute.For<ISearchScraper>();

            searchScraper.Scrape<IEntity>(Arg.Any<string>())
                         .Returns(new[] { new Company("Test") });

            var executor = new QueryExecutor<IEntity>(webClient, searchScraper);

            var queryDefinition = new QueryDefinition<IMovie> { Text = "sherlock" };

            var entities = executor.Execute(queryDefinition);

            entities.Should().NotBeEmpty();
        }
    }
}