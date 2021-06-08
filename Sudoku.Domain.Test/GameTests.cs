using System;
using System.Drawing;
using Sudoku.Common.Extensions;
using Sudoku.Data;
using Sudoku.Domain.Enums;
using Xunit;

namespace Sudoku.Domain.Test
{
    public class GameTests
    {
        [Fact]
        public void Should_Throw_ArgumentException_For_Point()
        {
            // Arrange 
            var game = new GameReader().Read("./Resources/empty.9x9");
            
            // Act & Assert
            Assert.Throws<ArgumentException>("point", () =>
            {
                game.Place(new Point(10, 10), 5);
            });
        }

        
        [Fact]
        public void Should_Be_Definite_EditorState()
        {
            // Arrange 
            var game = new GameReader().Read("./Resources/empty.9x9");

            // Assert
            Assert.Equal(EditorState.DefinitiveNumbers, game.State);
        }

        [Fact]
        public void Should_Be_Auxiliary_EditorState()
        {
            // Arrange
            var game = new GameReader().Read("./Resources/empty.9x9");
            
            // Act
            game.ToggleState();
        
            // Assert
            Assert.Equal(EditorState.AuxiliaryNumbers, game.State);
        }

        [Fact]
        public void Should_Toggle_To_Definite_EditorState()
        {
            // Arrange
            var game = new GameReader().Read("./Resources/empty.9x9");
            
            // Assert
            game.ToggleState();
            game.ToggleState();
        
            // Assert
            Assert.Equal(EditorState.DefinitiveNumbers, game.State);
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
        public void Should_Place_Number_On_Puzzle(int x, int y, int number, string path)
        {
            // Arrange
            var game = new GameReader().Read(path);

            // Act
            game.Place(new Point(x, y), number);
            
            // Assert
            Assert.False(game.Errors.Get(x, y));
            Assert.Equal(number, game.Cells.Get(x, y).Definite);
        }

        [Fact]
        public void Should_Not_Place_Double_On_SubGrid()
        {
            // Arrange
            var game = new GameReader().Read("./Resources/empty.9x9");

            // Act
            game.Place(new Point(5, 4), 6);
            game.Place(new Point(4, 4), 6);
            
            // Assert
            Assert.False(game.Errors.Get(5, 4));
            Assert.True(game.Errors.Get(4, 4));
            Assert.Equal(6, game.Cells.Get(5, 4).Definite);
            Assert.Equal(6, game.Cells.Get(4, 4).Definite);
        }
        
        [Fact]
        public void Should_Not_Place_Double_On_Line()
        {
            // Arrange
            var game = new GameReader().Read("./Resources/empty.9x9");

            // Act
            game.Place(new Point(1, 4), 6);
            game.Place(new Point(7, 4), 6);
            
            // Assert
            Assert.False(game.Errors.Get(1, 4));
            Assert.True(game.Errors.Get(7, 4));
            Assert.Equal(6, game.Cells.Get(1, 4).Definite);
            Assert.Equal(6, game.Cells.Get(7, 4).Definite);
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
        
            // Act
            game.Place(new Point(x, y), number);
            
            // Assert
            Assert.Null(game.Errors.Get(x, y));
            Assert.Equal(number, game.Cells.Get(x, y).Auxiliary[number - 1]);
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

            // Act
            game.Place(new Point(x, y), number);
            game.Place(new Point(x, y), replace);
            
            // Assert
            Assert.False(game.Errors.Get(x, y));
            Assert.Equal(replace, game.Cells.Get(x, y).Definite);
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

            // Act
            game.Place(new Point(x, y), number);
            game.Place(new Point(x, y), number);
            
            // Assert
            Assert.False(game.Errors.Get(x, y));
            Assert.Equal(0, game.Cells.Get(x, y).Definite);
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
        
            // Act
            game.Place(new Point(x, y), number);
            game.Place(new Point(x, y), number);
            
            // Assert
            Assert.Null(game.Errors.Get(x, y));
            Assert.Equal(0, game.Cells.Get(x, y).Auxiliary[number - 1]);
        }
    }
}