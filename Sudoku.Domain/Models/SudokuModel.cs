using System.Drawing;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Solvers;

namespace Sudoku.Domain.Models
{
    public class SudokuModel
    {
        private readonly IGrid _grid;
        public ICell[,] Cells { get; }
        public SudokuModel(int width, int height, IGrid grid)
        {
            var cells = new ICell[width, height];
            _grid = grid;
            _grid.Layout(cells);
            Cells = cells;
        }

        public void Accept(ISolver solver)
        {
            solver.Visit(this);
        }
        
        public bool Place(Point point, int number, bool temporary)
        {
            return _grid.Place(point, number, temporary);
        }
    }
}