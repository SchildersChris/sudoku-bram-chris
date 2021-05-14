using System.Collections.Generic;

namespace Sudoku.Domain.Models.Interfaces
{
    public interface IReadonlyGrid
    {
        IEnumerable<ICell[]> GetCells();
    }
}