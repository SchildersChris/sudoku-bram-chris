using System;
using Sudoku.Frontend.Controllers;

namespace Sudoku.Frontend
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var sudoku = new SudokuController();

            ConsoleKey key;
            while ((key = Console.ReadKey(true).Key) != ConsoleKey.Escape)
            {
                sudoku.Update(key);
            }
        }
    }
}