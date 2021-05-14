using Sudoku.Domain.Models;

namespace Sudoku.Domain.Solvers
{
    public interface ISolver
    {
        void Visit(SudokuModel sudoku);
    }
}