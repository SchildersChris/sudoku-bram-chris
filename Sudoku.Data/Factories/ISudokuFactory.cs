using System.Collections.Generic;
using Sudoku.Domain;
using Sudoku.Domain.Models;

namespace Sudoku.Data.Factories
{
    public interface ISudokuFactory
    {
        IGame Create(IEnumerable<string> lines);
    }
}