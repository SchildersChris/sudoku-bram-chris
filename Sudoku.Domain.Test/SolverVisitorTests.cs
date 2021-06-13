using System.Drawing;
using System.Linq;
using Sudoku.Common.Extensions;
using Sudoku.Data;
using Sudoku.Domain.Composite.Interfaces;
using Sudoku.Domain.Visitors;
using Xunit;

namespace Sudoku.Domain.Test
{
    public class SolverVisitorTests
    {
        [Theory]
        [InlineData("./Resources/puzzle.4x4")]
        [InlineData("./Resources/puzzle.6x6")]
        [InlineData("./Resources/puzzle.9x9")]
        [InlineData("./Resources/puzzle.jigsaw")]
        [InlineData("./Resources/puzzle.samurai")]
        public void Should_Solve_Sudoku(string path)
        {
            // Arrange
            var game = new GameReader().Read(path);
            var solver1 = new BoxLogicSolverVisitor();
            var solver2 = new BackTrackingSolverVisitor();
            
            // Act
            game.Accept(solver1);
            game.Accept(solver2);
            
            Assert.All(game.Cells.Cast<ICell>(), c =>
            {
                if (c == null)
                {
                    return;
                }
                
                Assert.NotEqual(0, c.Definite);
                Assert.Null(c.Error);
            });           
        }
        
        
        [Theory]
        [InlineData("./Resources/puzzle.4x4")]
        [InlineData("./Resources/puzzle.6x6")]
        [InlineData("./Resources/puzzle.9x9")]
        [InlineData("./Resources/puzzle.jigsaw")]
        [InlineData("./Resources/puzzle.samurai")]
        public void Should_Solve_Sudoku_After_Edit(string path)
        {
            // Arrange
            var game = new GameReader().Read(path);
            var solver1 = new BoxLogicSolverVisitor();
            var solver2 = new BackTrackingSolverVisitor();
            
            
            // Act
            game.Place(new Point(1, 3), 1);
            game.Place(new Point(2, 3), 3);
            game.Place(new Point(3, 3), 3);

            game.Accept(solver1);
            game.Accept(solver2);
            
            Assert.All(game.Cells.Cast<ICell>(), c =>
            {
                if (c == null)
                {
                    return;
                }
                
                Assert.NotEqual(0, c.Definite);
                Assert.Null(c.Error);
            });           
            
                        
            Assert.NotEqual(1, game.Cells.Get(new Point(1, 3)).Definite);
            Assert.NotEqual(3, game.Cells.Get(new Point(2, 3)).Definite);
            Assert.NotEqual(3, game.Cells.Get(new Point(3, 3)).Definite);
        }
        
        [Theory]
        [InlineData("./Resources/puzzle.4x4")]
        [InlineData("./Resources/puzzle.6x6")]
        [InlineData("./Resources/puzzle.9x9")]
        [InlineData("./Resources/puzzle.jigsaw")]
        [InlineData("./Resources/puzzle.samurai")]
        public void Should_Edit_Sudoku_After_Solve(string path)
        {
            // Arrange
            var game = new GameReader().Read(path);
            var solver1 = new BoxLogicSolverVisitor();
            var solver2 = new BackTrackingSolverVisitor();
            
            
            // Act
            game.Accept(solver1);
            game.Accept(solver2);
            
            game.Place(new Point(1, 3), 1);
            game.Place(new Point(2, 3), 2);
            game.Place(new Point(3, 3), 3);

            game.Accept(solver1);
            game.Accept(solver2);
            
            Assert.All(game.Cells.Cast<ICell>(), c =>
            {
                if (c == null)
                {
                    return;
                }
                
                Assert.NotEqual(0, c.Definite);
                Assert.Null(c.Error);
            });
            
            Assert.NotEqual(1, game.Cells.Get(new Point(1, 3)).Definite);
            Assert.NotEqual(2, game.Cells.Get(new Point(2, 3)).Definite);
            Assert.NotEqual(3, game.Cells.Get(new Point(3, 3)).Definite);
        }
    }
}