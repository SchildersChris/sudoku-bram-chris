using System;
using System.IO;
using Xunit;

namespace Sudoku.Data.Test
{
    public class GameReaderTests
    {
        [Theory]
        [InlineData(4, "4x4")]
        [InlineData(6, "6x6")]
        [InlineData(9, "9x9")]
        [InlineData(21, "samurai")]
        [InlineData(9, "jigsaw")]
        public void Should_Use_Correct_Strategy(int length, string extension)
        {
            // Arrange 
            var gameReader = new GameReader();
            
            // Act
            var game = gameReader.Read($"./Resources/puzzle.{extension}");

            // Assert
            Assert.Equal(length, game.Cells.GetLength(0));
            Assert.Equal(length, game.Cells.GetLength(1));
        }

        [Theory]
        [InlineData("./Resources/puzzle.nonExisting")]
        [InlineData("./Resources/faulty.nonExisting2")]
        public void Should_Throw_ArgumentException(string path)
        {
            // Arrange 
            var gameReader = new GameReader();
            
            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
            {
                gameReader.Read(path);
            });
        }
        
        [Fact]
        public void Should_Throw_FileNotFoundException()
        {
            // Arrange 
            var gameReader = new GameReader();
            
            // Act & Assert
            var ex = Assert.Throws<Exception>(() =>
            {
                
                gameReader.Read("./nonExistingPath.4x4");
            });

            Assert.IsType<FileNotFoundException>(ex.InnerException);
        }
    }
}