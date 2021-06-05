using System.Drawing;

namespace Sudoku.Common.Extensions
{
    public static class ArrayExtension 
    {
        public static bool Contains<T>(this T[,] origin, Point point)
        {
            return 0 <= point.Y && point.X < origin.GetLength(1) && 
                   0 <= point.Y && point.Y < origin.GetLength(0);
        }
    }
}