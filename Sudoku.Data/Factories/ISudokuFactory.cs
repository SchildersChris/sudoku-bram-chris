using Sudoku.Domain.Models;

namespace Sudoku.Data.Factories
{
    public interface ISudokuFactory
    {
        SudokuModel Create();
    }
}