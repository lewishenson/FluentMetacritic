using FluentAssertions;
using FluentMetacritic.Domain;
using FluentMetacritic.Query;
using System;
using System.Reflection;
using Xunit;
using Xunit.Extensions;

namespace FluentMetacritic.UnitTests.Query
{
    [Trait("Category", "UnitTest")]
    public class CategoryTests
    {
        [Theory]
        [InlineData(typeof(IEntity), "All")]
        [InlineData(typeof(IAlbum), "Albums")]
        [InlineData(typeof(ICompany), "Companies")]
        [InlineData(typeof(IFeature), "Features")]
        [InlineData(typeof(IGame), "Games")]
        [InlineData(typeof(IMovie), "Movies")]
        [InlineData(typeof(IPerson), "People")]
        [InlineData(typeof(ITelevisionShow), "TV Shows")]
        [InlineData(typeof(ITrailer), "Trailers")]
        public void GivenAnEntityType_WhenFromEntityIsCalled_ThenACategoryIsReturned(Type type, string expectedCategoryName)
        {
            var fromEntityMethod = typeof(Category).GetMethod("FromEntity", BindingFlags.Static | BindingFlags.Public);
            var genericFromEntityMethod = fromEntityMethod.MakeGenericMethod(type);

            var category = (Category)genericFromEntityMethod.Invoke(null, null);

            category.Name.ShouldBeEquivalentTo(expectedCategoryName);
        }
    }
}