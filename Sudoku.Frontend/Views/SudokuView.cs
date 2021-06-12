using System;
using System.Text;
using Sudoku.Common.Extensions;
using Sudoku.Frontend.Models;

namespace Sudoku.Frontend.Views
{
    public class SudokuView
    {
        private readonly SudokuModel _model;

        public SudokuView(SudokuModel model)
        {
            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Playing!\n");

            _model = model;
        }

        public void Update()
        {
            Console.Clear();
            // Console.SetCursorPosition(0, 2);
            // Console.CursorVisible = true;
            
            var width = _model.Cells.GetWidth();
            var height = _model.Cells.GetHeight();
            
            var bufferHeight = height * 2 - 1;
            var bufferWidth = width * 2 - 1;
            var buffer = new string[bufferHeight, bufferWidth];
            
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                   var c = _model.Cells.Get(x, y);
            
                   var y2 = y * 2;
                   var x2 = x * 2;
                   
                   if (c == null)
                   {
                       buffer.Set(x2, y2, " ");
                       continue;
                   }
                   
                   buffer.Set(x2, y2, c.ToString());
                   
                   // Check for horizontal border
                   if (_model.Cells.Contains(x + 1, y) && 
                       _model.Cells.Get(x + 1, y) != null && 
                       c.GridNumber != _model.Cells.Get(x + 1, y).GridNumber)
                   {
                       buffer.Set(x2 + 1, y2, "|");
                   }
            
                   // Check for vertical border
                   if (_model.Cells.Contains(x, y + 1) && 
                       _model.Cells.Get(x, y + 1) != null &&
                       c.GridNumber != _model.Cells.Get(x, y + 1).GridNumber)
                   {
                       buffer.Set(x2, y2 + 1, "-");
                   }
                }
            }
            
            for (var y = 0; y < buffer.GetHeight(); y++)
            {
                for (var x = 0; x < buffer.GetWidth(); x++)
                {
                    Console.Write(buffer.Get(x, y) ?? " ");
                }
                Console.Write('\n');
            }
            
            
            // // TODO: From this point remove old printing
            // Console.WriteLine("\n");
            // Console.WriteLine("\n");
            // Console.WriteLine("\n");
            // Console.WriteLine("\n");
            // Console.WriteLine("\n");
            Console.WriteLine("\n");
            
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var cell = _model.Cells.Get(x, y);
                    Console.Write(cell?.ToString() ?? " ");
                }
            
                Console.Write('\n');
            }
            
            Console.WriteLine($"\nCurrent view mode: {_model.State.ToString()} ");
        }
    }
}