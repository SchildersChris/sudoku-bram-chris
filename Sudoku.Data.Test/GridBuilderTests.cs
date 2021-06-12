using System.Drawing;
using Sudoku.Domain.Composite;
using Sudoku.Domain.Composite.Interfaces;
using Xunit;

namespace Sudoku.Data.Test
{
    public class GridBuilderTests
    {
        [Fact]
        public void Should_Have_Grid_Root()
        {
            // Arrange
            var gridBuilder = new GridBuilder(0);

            // Act
            var grid = gridBuilder.Build();

            // Assert
            Assert.IsType<GridComposite>(grid);
        }

        [Fact]
        public void Should_Layout_Tree()
        {
            // Arrange
            var gridBuilder = new GridBuilder(0);
            const int width = 5;
            const int height = 5;

            // Act
            var sudokuGrid = gridBuilder.AddSudokuGrid(new Rectangle(0, 0, width, height));
            for (var y = 0; y < height; y++)
            {
                var subGrid = sudokuGrid.AddGrid(y);
                for (var x = 0; x < width; x++)
                {
                    subGrid.AddCell(new Point(x, y), y * width + x);
                }
            }
            
            var grid = gridBuilder.Build();
            var cells = new ICell[height, width];
            grid.Layout(cells);

            // Assert
            var i = 0;
            for (var y = 0; y < cells.GetLength(0); y++)
            {
                for (var x = 0; x < cells.GetLength(1); x++)
                {
                    Assert.Equal(cells[y, x].GridNumber, y);
                    Assert.Equal(cells[y, x].Definite, i++);
                }
            }
        }
    }
}