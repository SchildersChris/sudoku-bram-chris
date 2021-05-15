using System.Drawing;

namespace Sudoku.Domain.Models.Interfaces
{
    public interface IGrid
    {
        int Count();
        bool Check(int number);
        bool Check(Point point, int number);
        bool Place(Point point, int number, bool temporary);
        void Layout(ICell[,] cells);
    }
}