using FluentAssertions;
using FluentMetacritic.Domain;
using System;
using Xunit;
using Xunit.Extensions;

namespace FluentMetacritic.UnitTests.Domain
{
    [Trait("Category", "UnitTest")]
    public class EntityTests
    {
        [Theory, InlineData(null), InlineData(""), InlineData(" ")]
        public void GivenAnInvalidNameIsUsed_WhenTheEntityIsConstructed_AnArgumentExceptionIsThrown(string name)
        {
            Action constructAction = () => new TestEntity(name);

            constructAction.ShouldThrow<ArgumentException>()
                           .WithMessage("Name is required.\r\nParameter name: name");
        }

        [Fact]
        public void GivenAValidNameIsUsed_WhenTheEntityIsConstructed_ThenTheNameWillBeSet()
        {
            var entity = new TestEntity(" Name ");

            entity.Name.ShouldBeEquivalentTo("Name");
        }

        private class TestEntity : Entity
        {
            public TestEntity(string name)
                : base(name)
            {
            }
        }
    }
}