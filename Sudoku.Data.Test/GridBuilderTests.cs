using System.Drawing;
using Sudoku.Domain.Models.Interfaces;
using Xunit;

namespace Sudoku.Data.Test
{
    public class GridBuilderTests
    {
        [Fact]
        public void Should_Layout_Simple_Tree()
        {
            // Arrange
            var gridBuilder = new GridBuilder();

            // Act
            var (width, height) = SetupSimpleTree(gridBuilder);

            // Assert
            var grid = gridBuilder.Build();
            var cells = new ICell[height, width];
            grid.Layout(cells);

            var i = 1;
            for (var y = 0; y < cells.GetLength(0); y++)
            {
                for (var x = 0; x < cells.GetLength(1); x++)
                {
                    Assert.Equal(cells[y, x].Definite, i++);
                }
            }
            
        }

        private static (int, int) SetupSimpleTree(GridBuilder gridBuilder)
        {
            const int width = 5;
            const int height = 1;
            
            var sudokuGrid = gridBuilder.AddSudokuGrid(new Rectangle(0, 0, width, 1));
            sudokuGrid.AddSubGrid()
                .AddCell(new Point(0, 0), 1)
                .AddCell(new Point(1, 0), 2)
                .AddCell(new Point(2, 0), 3)
                .AddCell(new Point(3, 0), 4)
                .AddCell(new Point(4, 0), 5);
            
            sudokuGrid.AddSubGrid()
                .AddCell(new Point(0, 1), 6)
                .AddCell(new Point(1, 1), 7)
                .AddCell(new Point(2, 1), 8)
                .AddCell(new Point(3, 1), 9)
                .AddCell(new Point(4, 1), 10);

            return (width, height);
        }
    }
}