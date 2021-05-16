using System.Collections.Generic;
using Sudoku.Domain.Models;

namespace Sudoku.Data.Factories
{
    public interface ISudokuFactory
    {
        SudokuModel Create(IEnumerable<string> lines);
    }
}