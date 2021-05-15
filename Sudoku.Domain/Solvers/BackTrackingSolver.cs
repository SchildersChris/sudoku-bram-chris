using Sudoku.Domain.Models;

namespace Sudoku.Domain.Solvers
{
    public class BackTrackingSolver : ISolver
    {
        public void Visit(SudokuModel sudoku)
        {
            var cells = sudoku.Cells;
            foreach (var cell in cells)
            {
                // Todo: Add backtracking ding
            }
        }
    }
}