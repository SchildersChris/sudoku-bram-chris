using System.Drawing;

namespace Sudoku.Domain.Models.Interfaces
{
    public interface IGrid
    {
        bool Check(int number);
        bool Place(Point point, int number, bool temporary);
        void Layout(ICell[,] cells);
    }
}