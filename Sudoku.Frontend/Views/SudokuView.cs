using System;
using System.Drawing;
using System.Text;
using Pastel;
using Sudoku.Common.Extensions;
using Sudoku.Domain.Composite.Interfaces;
using Sudoku.Domain.Enums;
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

            Console.WriteLine($"\nCurrent view mode: {_model.State.ToString()} ");
        }


        private string[,] CreateSudoku()
        {
            var cells = _model.Cells;
            var height = cells.GetHeight();
            var width = cells.GetWidth();

            int col = 1, row = 1;
            if (_model.State == EditorState.AuxiliaryNumbers)
            {
                 (col, row) = width.Factorize();
            }
            
            var bufferHeight = height * row * 2 - 1;
            var bufferWidth = width * col * 2 - 1;
            var buffer = new string[bufferHeight, bufferWidth];

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var c = cells.Get(x, y);
            
                    var y2 = y * row * 2;
                    var x2 = x * col * 2;

                    if (_model.State == EditorState.DefinitiveNumbers)
                    {
                        WriteDefiniteCell(c, buffer, x2, y2);
                    }
                    else
                    {
                        WriteAuxiliaryCell(c, buffer, x2, y2);
                    }

                    if (c == null)
                    {
                        continue;
                    }

                    // Check for horizontal border
                    if (cells.Contains(x + 1, y) && 
                        cells.Get(x + 1, y) != null && 
                        c.GridNumber != cells.Get(x + 1, y).GridNumber)
                    {
                        buffer.Set(x2 + 1, y2, "|".Pastel(Color.OrangeRed));
                    }
            
                    // Check for vertical border
                    if (cells.Contains(x, y + 1) && 
                        cells.Get(x, y + 1) != null &&
                        c.GridNumber != cells.Get(x, y + 1).GridNumber)
                    {
                        buffer.Set(x2, y2 + 1, "-".Pastel(Color.OrangeRed));
                    }

                    // Selector background
                    if (_model.Position.X == x && _model.Position.Y == y)
                    {
                        buffer.Set(x2, y2, buffer.Get(x2, y2).PastelBg(Color.DarkGray).Pastel(Color.Black));
                    }
                }
            }
            
            return buffer;
        }
        
        private static void WriteDefiniteCell(ICell cell, string[,] buffer, int xStart, int yStart)
        {
            if (cell == null)
            {
                buffer.Set(xStart, yStart, " ");
                return;
            }

            var val = cell.Definite != 0 ? cell.Definite.ToString() : ".";
            if (cell.Error == true)
            {
                val = val.Pastel(Color.White).PastelBg(Color.Red);
            }
            
            buffer.Set(xStart, yStart, val);
        }
        
        private static void WriteAuxiliaryCell(ICell cell, string[,] buffer, int xStart, int yStart)
        {
            var val = cell.Definite != 0 ? cell.Definite.ToString() : ".";
            if (cell.Error == true)
            {
                val = val.Pastel(Color.White).PastelBg(Color.Red);
            }
        }
    }
}