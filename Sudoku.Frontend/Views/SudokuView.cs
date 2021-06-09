﻿using System;
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
            Console.SetCursorPosition(0, 2);
            Console.CursorVisible = false;

            var width = _model.Cells.GetWidth();
            var height = _model.Cells.GetHeight();
            
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var cell = _model.Cells.Get(x, y);
                    Console.Write(cell?.ToString() ?? "|");
                }

                Console.Write('\n');
            }
            
            Console.WriteLine($"\nCurrent view mode: {_model.State.ToString()} ");
        }
    }
}