using System.Collections.Generic;
using Sudoku.Domain;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Data.Factories
{
    public interface ISudokuFactory
    {
        (int, IGrid) Create(IEnumerable<string> lines);
    }
}