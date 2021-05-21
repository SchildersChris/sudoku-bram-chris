using System.Drawing;
using Sudoku.Data;
using Sudoku.Domain.Enums;
using Xunit;

namespace Sudoku.Domain.Test
{
    public class GameTests
    {
        [Theory]
        [InlineData(1, 3, 1, "./Resources/empty.samurai")]
        [InlineData(2, 4, 2, "./Resources/empty.samurai")]
        [InlineData(3, 5, 3, "./Resources/empty.samurai")]
        [InlineData(4, 6, 4, "./Resources/empty.samurai")]
        [InlineData(12, 7, 4, "./Resources/empty.samurai")]
        [InlineData(13, 8, 4, "./Resources/empty.samurai")]
        [InlineData(14, 6, 4, "./Resources/empty.samurai")]
        [InlineData(1, 3, 1, "./Resources/empty.4x4")]
        [InlineData(2, 4, 2, "./Resources/empty.9x9")]
        [InlineData(3, 5, 3, "./Resources/empty.6x6")]
        public void Should_Place_Number_On_Puzzle(int x, int y, int number, string path)
        {
            // Arrange
            var game = new GameReader().Read(path);

            // Act & Assert
            Assert.True(game.Place(new Point(x, y), number));
            Assert.Equal(number, game.Cells[y, x].Definite);
        }
        
        [Theory]
        [InlineData(1, 3, 1, "./Resources/empty.samurai")]
        [InlineData(2, 4, 2, "./Resources/empty.samurai")]
        [InlineData(3, 5, 3, "./Resources/empty.samurai")]
        [InlineData(4, 6, 4, "./Resources/empty.samurai")]
        [InlineData(12, 7, 4, "./Resources/empty.samurai")]
        [InlineData(13, 8, 4, "./Resources/empty.samurai")]
        [InlineData(14, 6, 4, "./Resources/empty.samurai")]
        [InlineData(1, 3, 1, "./Resources/empty.4x4")]
        [InlineData(2, 4, 2, "./Resources/empty.9x9")]
        [InlineData(3, 5, 3, "./Resources/empty.6x6")]
        public void Should_Place_Auxiliary_On_Puzzle(int x, int y, int number, string path)
        {
            // Arrange
            var game = new GameReader().Read(path);
            if (game.State != EditorState.AuxiliaryNumbers)
            {
                game.ToggleState();
            }
        
            // Act & Assert
            Assert.True(game.Place(new Point(x, y), number));
            Assert.Equal(number, game.Cells[y, x].Auxiliary[number - 1]);
        }
        
        [Theory]
        [InlineData(1, 3, 1, 2, "./Resources/empty.samurai")]
        [InlineData(2, 4, 2, 3, "./Resources/empty.samurai")]
        [InlineData(3, 5, 3, 4, "./Resources/empty.samurai")]
        [InlineData(4, 6, 4, 5, "./Resources/empty.samurai")]
        [InlineData(12, 7, 4, 5, "./Resources/empty.samurai")]
        [InlineData(13, 8, 4, 5, "./Resources/empty.samurai")]
        [InlineData(14, 6, 4, 5, "./Resources/empty.samurai")]
        [InlineData(1, 3, 1, 2, "./Resources/empty.4x4")]
        [InlineData(2, 4, 2, 3, "./Resources/empty.9x9")]
        [InlineData(3, 5, 3, 4, "./Resources/empty.6x6")]
        public void Should_Replace_Number_On_Puzzle(int x, int y, int number, int replace, string path)
        {
            // Arrange
            var game = new GameReader().Read(path);

            // Act & Assert
            Assert.True(game.Place(new Point(x, y), number));
            Assert.True(game.Place(new Point(x, y), replace));
            Assert.Equal(replace, game.Cells[y, x].Definite);
        }
        
        
        [Theory]
        [InlineData(1, 3, 1, "./Resources/empty.samurai")]
        [InlineData(2, 4, 2, "./Resources/empty.samurai")]
        [InlineData(3, 5, 3, "./Resources/empty.samurai")]
        [InlineData(4, 6, 4, "./Resources/empty.samurai")]
        [InlineData(12, 7, 4, "./Resources/empty.samurai")]
        [InlineData(13, 8, 4, "./Resources/empty.samurai")]
        [InlineData(14, 6, 4, "./Resources/empty.samurai")]
        [InlineData(1, 3, 1, "./Resources/empty.4x4")]
        [InlineData(2, 4, 2, "./Resources/empty.9x9")]
        [InlineData(3, 5, 3, "./Resources/empty.6x6")]
        public void Should_Remove_Number_On_Puzzle(int x, int y, int number, string path)
        {
            // Arrange
            var game = new GameReader().Read(path);

            // Act & Assert
            Assert.True(game.Place(new Point(x, y), number));
            Assert.False(game.Place(new Point(x, y), number));
            Assert.Equal(0, game.Cells[y, x].Definite);
        }
        
        [Theory]
        [InlineData(1, 3, 1, "./Resources/empty.samurai")]
        [InlineData(2, 4, 2, "./Resources/empty.samurai")]
        [InlineData(3, 5, 3, "./Resources/empty.samurai")]
        [InlineData(4, 6, 4, "./Resources/empty.samurai")]
        [InlineData(12, 7, 4, "./Resources/empty.samurai")]
        [InlineData(13, 8, 4, "./Resources/empty.samurai")]
        [InlineData(14, 6, 4, "./Resources/empty.samurai")]
        [InlineData(1, 3, 1, "./Resources/empty.4x4")]
        [InlineData(2, 4, 2, "./Resources/empty.9x9")]
        [InlineData(3, 5, 3, "./Resources/empty.6x6")]
        public void Should_Remove_Auxiliary_On_Puzzle(int x, int y, int number, string path)
        {
            // Arrange
            var game = new GameReader().Read(path);
            if (game.State != EditorState.AuxiliaryNumbers)
            {
                game.ToggleState();
            }
        
            // Act & Assert
            Assert.True(game.Place(new Point(x, y), number));
            Assert.True(game.Place(new Point(x, y), number));
            Assert.Equal(0, game.Cells[y, x].Auxiliary[number - 1]);
        }
    }
}