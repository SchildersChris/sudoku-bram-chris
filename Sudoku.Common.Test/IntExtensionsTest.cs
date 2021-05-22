using System;
using Sudoku.Common.Extensions;
using Xunit;

namespace Sudoku.Common.Test
{
    public class IntExtensionsTest
    {
        [Theory]
        [InlineData(6, 3, 2)]
        [InlineData(15, 5, 3)]
        [InlineData(28, 7, 4)]
        [InlineData(8, 4, 2)]
        public void Should_Factorize_Number(int number, int factor1, int factor2)
        {
            // Act
            var (a, b) = number.Factorize();
            
            // Assert
            Assert.Equal(factor1, a);
            Assert.Equal(factor2, b);
        }
        
        [Theory]
        [InlineData(29)]
        [InlineData(31)]
        public void Should_Be_Unable_To_Factorize_Number(int number)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>("origin",() =>
            {
                number.Factorize();
            });
        }
    }
}