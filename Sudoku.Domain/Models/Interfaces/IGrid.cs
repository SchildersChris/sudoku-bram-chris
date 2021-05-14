using System.Drawing;

namespace Sudoku.Domain.Models.Interfaces
{
    public interface IGrid : IReadonlyGrid
    {
        bool Check(Point point, int number);
        bool Place(Point point, int number, bool temporary);
    }
}