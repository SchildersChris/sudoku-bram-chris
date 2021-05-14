using Sudoku.Frontend.Controllers;

namespace Sudoku.Frontend
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            App.Instance.Run(new StartController());
        }
    }
}