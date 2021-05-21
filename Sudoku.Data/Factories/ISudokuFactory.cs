using System.Collections.Generic;
using Sudoku.Domain.Composite.Interfaces;

namespace Sudoku.Data.Factories
{
    public interface ISudokuFactory
    {
        (int, IGridComponent) Create(IEnumerable<string> lines);
    }
}