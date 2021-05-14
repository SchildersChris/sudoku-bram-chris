using Sudoku.Domain.Models;

namespace Sudoku.Domain.Solvers
{
    public class BackTrackingSolver : ISolver
    {
        public void Visit(SudokuModel sudoku)
        {
            var grids = sudoku.Grids;

            foreach (var grid in grids)
            {
                var cells = grid.GetCells();
                foreach (var cell in cells)
                {
                    // Todo: Add backtracking ding
                }
            }
        }
    }
}