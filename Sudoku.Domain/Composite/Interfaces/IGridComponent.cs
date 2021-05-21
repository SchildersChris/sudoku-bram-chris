using System.Drawing;

namespace Sudoku.Domain.Composite.Interfaces
{
    public interface IGridComponent
    {
        bool CheckInverted(Point point, int number);
        bool Place(Point point, int number, bool temporary);
        void Layout(ICell[,] cells);
    }
}