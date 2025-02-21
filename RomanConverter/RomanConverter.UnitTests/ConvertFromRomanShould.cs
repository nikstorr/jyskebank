using Xunit;
using RomanConverter.Core;

namespace RomanConverter.UnitTests
{
    public class ConvertFromRomanShould
    {
        [Theory]
        [InlineData("MMM", 3000)]
        [InlineData("MCMXCIX", 1999)]
        [InlineData("MMCDXLIV", 2444)]
        public void ConvertCorrectly(string actual, int expected)
        {
            // Arrange
            var sut = new ConvertFromRoman();

            // Act
            var result = sut.Convert(actual);

            // Assert
            Assert.True(result == expected);
        }

        [Fact]
        public void AddSimilarNumerals()
        {
            // Arrange
            var sut = new ConvertFromRoman();
            
            var input = "XX";
            // Act

            var result = sut.Convert(input);

            // Assert
            Assert.True(result == 20);
        }
        
        [Fact]
        public void AddSmallerNumbersPositionedAfterBiggerNumbers()
        {
            // Arrange
            var sut = new ConvertFromRoman();

            var input = "XI";
            // Act

            var result = sut.Convert(input);

            // Assert
            Assert.True(result == 11);
        }

        [Fact]
        public void SubtractSmallerNumbersPositionedBeforeBiggerNumbers()
        {
            // Arrange
            var sut = new ConvertFromRoman();

            var input = "IX";
            // Act

            var result = sut.Convert(input);

            // Assert
            Assert.True(result == 9);
        }

        [Fact]
        public void HaveSingleNumeralBeforeLargerNumerals() 
        {
            // Arrange
            var sut = new ConvertFromRoman();

            var inputs = new string[] { "IIX", "IIVM", "VVX"};

            // Act
            // Assert
            foreach(var inp in inputs)
                Assert.Throws<ArgumentException>(() => sut.Convert(inp));

        }

        [Fact]
        public void Have_I_OnlyAppearingBefore_V_X()
        {
            // Arrange
            var sut = new ConvertFromRoman();

            var inputs = new string[] { "ID", "IM" };

            // Act
            // Assert
            foreach (var inp in inputs)
                Assert.Throws<ArgumentException>(() => sut.Convert(inp));
        }

        [Fact]
        public void Have_X_OnlyAppearingBefore_L_C()
        {
            // Arrange
            var sut = new ConvertFromRoman();

            var inputs = new string[] { "XD", "XV" };

            // Act
            // Assert
            foreach (var inp in inputs)
                Assert.Throws<ArgumentException>(() => sut.Convert(inp));
        }

        [Fact]
        public void Have_C_OnlyAppearingBefore_D_M()
        {
            // Arrange
            var sut = new ConvertFromRoman();

            var inputs = new string[] { "CX", "CV" };

            // Act
            // Assert
            foreach (var inp in inputs)
                Assert.Throws<ArgumentException>(() => sut.Convert(inp));
        }

        [Fact]
        public void HaveOnlyThreeIdenticalConcurrentNumerals() 
        {
            // Arrange
            var sut = new ConvertFromRoman();

            var inputs = new string[] { "CCCC", "IIII", "MMMMCMXCIX" };

            // Act
            // Assert
            foreach (var inp in inputs)
                Assert.Throws<ArgumentException>(() => sut.Convert(inp));
        }  

        [Fact]
        public void NotRepeat_V_L_D()
        {
            // Arrange
            var sut = new ConvertFromRoman();

            var inputs = new string[] { "DDD", "VVVV"};

            // Act
            // Assert
            foreach (var inp in inputs)
                Assert.Throws<ArgumentException>(() => sut.Convert(inp));
        }

        [Fact]
        public void HandleNonsenseInput()
        {
            // Arrange
            var sut = new ConvertFromRoman();

            var inputs = new string[] { "HG", "", "#%IVD", "-%&"};

            // Act
            // Assert
            foreach (var inp in inputs)
                Assert.Throws<ArgumentException>(() => sut.Convert(inp));

        }
    }
}
