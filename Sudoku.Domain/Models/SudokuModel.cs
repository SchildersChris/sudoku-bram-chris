using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Solvers;

namespace Sudoku.Domain.Models
{
    public class SudokuModel
    {
        private readonly IEnumerable<IGrid> _grids;
        public IEnumerable<IReadonlyGrid> Grids => _grids;

        public SudokuModel(IEnumerable<IGrid> grids)
        {
            _grids = grids;
        }

        public void Accept(ISolver solver)
        {
            solver.Visit(this);
        }
        
        public bool Place(Point point, int number, bool temporary)
        {
            return _grids.Any(grid => grid.Place(point, number, temporary));
        }
    }
}