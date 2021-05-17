using System;
using Sudoku.Frontend.Controllers;

namespace Sudoku.Frontend
{
    public class App
    {
        private IController _controller;
        public static App Instance { get; } = new();

        private App()
        {
        }

        public void Run(IController controller)
        {
            SetController(controller);

            ConsoleKey key;
            while ((key = Console.ReadKey(true).Key) != ConsoleKey.Escape)
            {
                _controller.Update(key);
            }
        }

        public void SetController(IController controller)
        {
            _controller = controller;
            _controller.Update(ConsoleKey.NoName);
        }
    }
}