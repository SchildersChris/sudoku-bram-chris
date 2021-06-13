using System;
using System.Drawing;
using System.Text;
using Pastel;
using Sudoku.Common.Extensions;
using Sudoku.Frontend.Models;

namespace Sudoku.Frontend.Views
{
    public abstract class SudokuView
    {
        protected readonly SudokuModel Model;

        protected SudokuView(SudokuModel model)
        {
            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Playing!\n");

            Model = model;
        }

        public void Update()
        {
            Console.Clear();
            Console.CursorVisible = false;

            var buffer = CreateSudoku();

            for (var y = 0; y < buffer.GetHeight(); y++)
            {
                for (var x = 0; x < buffer.GetWidth(); x++)
                {
                    Console.Write(buffer.Get(x, y) ?? " ");
                }
                Console.Write('\n');
            }

            Console.WriteLine($"\nCurrent view mode: {Model.State.ToString()} ");
            Console.WriteLine($"\nShow errors: {Model.ShowErrors.ToString()} ");
            if (Model.Error != null)
            {
                Console.WriteLine(Model.Error.Pastel(Color.OrangeRed));
            }
        }


        protected abstract string[,] CreateSudoku();
    }
}