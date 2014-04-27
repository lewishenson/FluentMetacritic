using FluentAssertions;

using FluentMetacritic.Scraping;

using Xunit;
using Xunit.Extensions;

namespace FluentMetacritic.UnitTests.Scraping
{
    [Trait("Category", "UnitTest")]
    public class ConverterTests
    {
        [Theory]
        [InlineData("7", 7)]
        [InlineData(null, 0)]
        [InlineData("", 0)]
        [InlineData(" ", 0)]
        public void GivenAStringInput_WhenConvertedToInteger_ItIsCorrect(string input, int expectedOutput)
        {
            var output = Converter.SafeConvert<int>(input);

            output.Should().Be(expectedOutput);
        }

        [Theory]
        [InlineData("7", 0, 7)]
        [InlineData(null, 3, 3)]
        [InlineData("", 2, 2)]
        [InlineData(" ", 1, 1)]
        public void GivenAStringInput_WhenConvertedToIntegerAndDefaultValueIsSupplied_ItIsCorrect(string input, int defaultValue, int expectedOutput)
        {
            var output = Converter.SafeConvert(input, defaultValue);

            output.Should().Be(expectedOutput);
        }

        [Theory]
        [InlineData("7", 7)]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData(" ", null)]
        public void GivenAStringInput_WhenConvertedToNullableInteger_ItIsCorrect(string input, int? expectedOutput)
        {
            var output = Converter.SafeConvert<int?>(input);

            output.Should().Be(expectedOutput);
        }

        [Theory]
        [InlineData("7", 0, 7)]
        [InlineData(null, 3, 3)]
        [InlineData("", null, null)]
        [InlineData(" ", 1, 1)]
        public void GivenAStringInput_WhenConvertedToNullableIntegerAndDefaultValueIsSupplied_ItIsCorrect(string input, int? defaultValue, int? expectedOutput)
        {
            var output = Converter.SafeConvert(input, defaultValue);

            output.Should().Be(expectedOutput);
        }
    }
}