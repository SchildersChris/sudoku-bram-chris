using System;
using System.Text;
using Sudoku.Frontend.Models;

namespace Sudoku.Frontend.Views
{
    public class StartView
    {
        private readonly StartModel _model;

        public StartView(StartModel model)
        {
            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;
            
            _model = model;
            Console.WriteLine("Welcome to Sudoku.\n\n");
        }
        
        public void Update()
        {
            if (_model.SudokuPath == null)
            {
                Console.Write("Please enter a sudoku file path:");
                _model.SudokuPath = Console.ReadLine();
            }
            
            Console.WriteLine("\nSettings:");
            Console.WriteLine($" - DisplayMode: {(_model.SimpleDisplay ? "Simple" : "Advanced" )}");
            Console.WriteLine($" - File Path: {_model.SudokuPath ?? "None"}");

            Console.WriteLine("\nOptions:");
            Console.WriteLine(" - To change the display mode press 'D'");
            Console.WriteLine(" - To change the file path mode press 'F'");
            Console.WriteLine(" - Press 'S' to start the game");
        }
    }
}