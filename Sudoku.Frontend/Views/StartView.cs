using System;
using System.Drawing;
using System.Text;
using Pastel;
using Sudoku.Frontend.Models;

namespace Sudoku.Frontend.Views
{
    public class StartView
    {
        private readonly StartModel _model;

        public StartView(StartModel model)
        {
            Console.OutputEncoding = Encoding.UTF8;
            _model = model;
        }

        public void Update()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Sudoku.\n");

            if (_model.SudokuPath == null)
            {
                Console.Write("Please enter a sudoku file path: ");
                _model.SudokuPath = Console.ReadLine();
            }
            
            Console.Clear();
            Console.WriteLine("Welcome to Sudoku.\n");
            
            Console.WriteLine("Settings:");
            Console.WriteLine($" - Display Mode: {(_model.SimpleDisplay ? "Simple" : "Advanced")}");
            Console.WriteLine($" - File Path: '{_model.SudokuPath ?? "None"}'");
            if (_model.Error != null)
            {
                Console.WriteLine(_model.Error.Pastel(Color.OrangeRed));
            }
            
            Console.WriteLine("\nOptions:");
            Console.WriteLine(" - To toggle the display mode press 'D'");
            Console.WriteLine(" - To change the file path mode press 'F'");
            Console.WriteLine(" - Press 'S' to start the game");
        }
    }
}