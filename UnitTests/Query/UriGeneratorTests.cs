using FluentAssertions;

using FluentMetacritic.Query;

using System;

using Xunit;
using Xunit.Extensions;

namespace FluentMetacritic.UnitTests.Query
{
    [Trait("Category", "UnitTest")]
    public class UriGeneratorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void GivenAnInvalidTerm_WhenTheUriIsGenerated_AnArgumentExceptionIsThrown(string searchTerm)
        {
            Action constructAction = () => UriGenerator.Create().Generate(searchTerm);

            constructAction.ShouldThrow<ArgumentException>()
                           .WithMessage("Search term is required.\r\nParameter name: searchTerm");
        }

        [Fact]
        public void GivenAValidTermAndNoCategoryAndNoOrderByAndNoPage_WhenTheUriIsGenerated_ThenItWillBeCorrect()
        {
            var result = UriGenerator.Create()
                                     .Generate(" star trek ");

            result.ShouldBeEquivalentTo("http://www.metacritic.com/search/all/star%2btrek/results");
        }

        [Fact]
        public void GivenAValidTermAndCategoryAndNoOrderByAndNoPage_WhenTheUriIsGenerated_ThenItWillBeCorrect()
        {
            var result = UriGenerator.Create()
                                     .ForCategory(Category.TelevisionShows)
                                     .Generate(" star trek ");

            result.ShouldBeEquivalentTo("http://www.metacritic.com/search/tv/star%2btrek/results");
        }

        [Fact]
        public void GivenAValidTermAndCategoryAndOrderByAndNoPage_WhenTheUriIsGenerated_ThenItWillBeCorrect()
        {
            var result = UriGenerator.Create()
                                     .ForCategory(Category.Movies)
                                     .OrderedBy(OrderBy.MostRecent)
                                     .Generate(" star trek ");

            result.ShouldBeEquivalentTo("http://www.metacritic.com/search/movie/star%2btrek/results?sort=recent");
        }

        [Fact]
        public void GivenAValidTermAndCategoryAndOrderByAndPage_WhenTheUriIsGenerated_ThenItWillBeCorrect()
        {
            var result = UriGenerator.Create()
                                     .ForCategory(Category.Games)
                                     .OrderedBy(OrderBy.MostRecent)
                                     .Page(2)
                                     .Generate(" star trek ");

            result.ShouldBeEquivalentTo("http://www.metacritic.com/search/game/star%2btrek/results?page=1&sort=recent");
        }
    }
}