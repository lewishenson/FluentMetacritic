using FluentAssertions;
using FluentMetacritic.DependencyInjection;
using FluentMetacritic.Domain;
using FluentMetacritic.Net;
using FluentMetacritic.Query;
using FluentMetacritic.Scraping;
using FluentMetacritic.Search;
using System;
using Xunit;
using Xunit.Sdk;

namespace FluentMetacritic.UnitTests.DependencyInjection
{
    [Trait("Category", "UnitTest")]
    public class FactoryTests
    {
        [Fact]
        public void GivenAFactorySingleton_WhenAnUnknownInstanceIsRequired_ThenAnExceptionIsThrown()
        {
            Action factoryAction = () => Factory.Instance.Create<TestClass>();

            factoryAction.ShouldThrow<InvalidOperationException>()
                         .WithMessage("Unable to create Xunit.Sdk.TestClass instance.");
        }

        [Theory]
        [InlineData(typeof(IMetacriticSearch))]
        [InlineData(typeof(IWebClient))]
        [InlineData(typeof(IQueryBuilder<IEntity>))]
        [InlineData(typeof(IQueryBuilder<IAlbum>))]
        [InlineData(typeof(IQueryBuilder<ICompany>))]
        [InlineData(typeof(IQueryBuilder<IGame>))]
        [InlineData(typeof(IQueryBuilder<IMovie>))]
        [InlineData(typeof(IQueryBuilder<IPerson>))]
        [InlineData(typeof(IQueryBuilder<ITrailer>))]
        [InlineData(typeof(IQueryBuilder<ITelevisionShow>))]
        [InlineData(typeof(IQueryDefinition<IEntity>))]
        [InlineData(typeof(IQueryDefinition<IAlbum>))]
        [InlineData(typeof(IQueryDefinition<ICompany>))]
        [InlineData(typeof(IQueryDefinition<IGame>))]
        [InlineData(typeof(IQueryDefinition<IMovie>))]
        [InlineData(typeof(IQueryDefinition<IPerson>))]
        [InlineData(typeof(IQueryDefinition<ITrailer>))]
        [InlineData(typeof(IQueryDefinition<ITelevisionShow>))]
        [InlineData(typeof(IQueryExecutor<IEntity>))]
        [InlineData(typeof(IQueryExecutor<IAlbum>))]
        [InlineData(typeof(IQueryExecutor<ICompany>))]
        [InlineData(typeof(IQueryExecutor<IGame>))]
        [InlineData(typeof(IQueryExecutor<IMovie>))]
        [InlineData(typeof(IQueryExecutor<IPerson>))]
        [InlineData(typeof(IQueryExecutor<ITrailer>))]
        [InlineData(typeof(IQueryExecutor<ITelevisionShow>))]
        [InlineData(typeof(ISearchScraper))]
        public void GivenAFactorySingleton_WhenAKnownTypeIsRequired_ThenItIsCreated(Type type)
        {
            var createMethod = typeof(Factory).GetMethod("Create");
            var genericCreateMethod = createMethod.MakeGenericMethod(type);

            var createdInstance = genericCreateMethod.Invoke(Factory.Instance, null);

            createdInstance.Should().NotBeNull();
        }
    }
}