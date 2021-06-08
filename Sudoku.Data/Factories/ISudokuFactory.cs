using System.Collections.Generic;
using Sudoku.Domain;

namespace Sudoku.Data.Factories
{
    public interface ISudokuFactory
    {
        IGameElement Create(IEnumerable<string> lines);
    }
}