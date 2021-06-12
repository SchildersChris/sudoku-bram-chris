using System.Drawing;

namespace Sudoku.Common.Extensions
{
    public static class ArrayExtension 
    {
        public static bool Contains<T>(this T[,] origin, Point point)
        {
            return 0 <= point.X && point.X < origin.GetWidth() && 
                   0 <= point.Y && point.Y < origin.GetHeight();
        }
        
        public static bool Contains<T>(this T[,] origin, int x, int y)
        {
            return 0 <= x && x < origin.GetWidth() && 
                   0 <= y && y < origin.GetHeight();
        }

        public static void Set<T>(this T[,] origin, Point point, T item)
        {
            origin[point.Y, point.X] = item;
        }
        
        public static void Set<T>(this T[,] origin, int x, int y, T item)
        {
            origin[y, x] = item;
        }
        
        public static T Get<T>(this T[,] origin, Point point)
        {
            return origin[point.Y, point.X];
        }
        
        public static T Get<T>(this T[,] origin, int x, int y)
        {
            return origin[y, x];
        }
        
        public static int GetWidth<T>(this T[,] origin)
        {
            return origin.GetLength(1);
        }
        
        public static int GetHeight<T>(this T[,] origin)
        {
            return origin.GetLength(0);
        }
    }
}