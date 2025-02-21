using Xunit;
using RomanConverter.Core;

namespace RomanConverter.UnitTests
{
    public class ConvertToRomanShould
    {
        [Theory]
        [InlineData(3000, "MMM")]
        [InlineData(1999, "MCMXCIX")]
        [InlineData(2444, "MMCDXLIV")]
        public void ConvertCorrectly(int actual, string expected)
        {
            // Arrange
            var sut = new ConvertToRoman();

            // Act
            var result = sut.Convert(actual);

            // Assert
            Assert.True(result == expected);
        }

        [Fact]
        public void HandleNonsenseInput()
        {
            var sut = new ConvertToRoman();

            Assert.Throws<ArgumentException>(() => sut.Convert(-451));
            Assert.Throws<ArgumentException>(() => sut.Convert(0));
        }
    }
}
