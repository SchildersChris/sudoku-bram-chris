using System;
using System.Collections.Generic;
using System.Text;
using Sudoku.Common.Extensions;
using Sudoku.Frontend.Models;
using System.Linq;

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
            Console.SetCursorPosition(0, 2);
            Console.CursorVisible = true;

            var grids = _model.Cells
                .Cast<CellModel>()
                .GroupBy(c => c?.GridNumber ?? -1)
                .Select(x => x.ToList()).ToList();
            
            var width = _model.Cells.GetWidth();
            var height = _model.Cells.GetHeight();
            var cursorHeight = 1;
            var cursorWidth = 1;

            for (var grid = 0; grid < grids.Count(); grid++)
            {
                var (gridWidth, gridHeight) = grids[grid].Count.Factorize();
                var index = 0;

                if (grid % gridWidth == 0)
                {
                    cursorHeight += grid;
                }

                if (grid % gridHeight != 0)
                {
                    cursorWidth += gridWidth;
                }
                else
                {
                    cursorWidth = 1;
                }

                Console.SetCursorPosition(cursorWidth, cursorHeight);

                for (var i = 0; i < gridHeight; i++)
                {
                    // Console.Write(" | ");
                    for (var j = 0; j < gridWidth; j++)
                    {
                        Console.Write(grids[grid][index].ToString() ?? "   ");
                        index++;
                    }

                    Console.SetCursorPosition(cursorWidth, cursorHeight + i + 1);
                }

                Console.Write("\n");
            }
            
            
            // TODO: From this point remove old printing
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine("\n");

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var cell = _model.Cells.Get(x, y);
                    Console.Write(cell?.ToString() ?? "   ");
                }
            
                Console.Write('\n');
            }
            
            Console.WriteLine($"\nCurrent view mode: {_model.State.ToString()} ");
        }
    }
}