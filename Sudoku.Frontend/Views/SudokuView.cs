using System;
using System.Text;
using Sudoku.Frontend.Models;

namespace Sudoku.Frontend.Views
{
    /// <summary>
    /// Sudoku view represents a class that visualizes the sudoku
    /// </summary>
    public class SudokuView
    {
        private readonly SudokuModel _model;
        
        public SudokuView(SudokuModel model)
        {
            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;
            
            _model = model;
        }

        public void Update()
        {
            foreach (var cell in _model.Cells)
            {
                var p = cell.Point;
                Console.SetCursorPosition(p.X, p.Y);
                Console.CursorVisible = false;
                Console.Write(cell.ToString());
            }
        }
    }
}