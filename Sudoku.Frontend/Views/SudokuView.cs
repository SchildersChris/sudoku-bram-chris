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
            Console.WriteLine($"\nShow errors: {_model.ShowErrors.ToString()} ");
        }


        private string[,] CreateSudoku()
        {
            var cells = _model.Cells;
            var height = cells.GetHeight();
            var width = cells.GetWidth();

            var col = 1;
            var row = 1;
            int bufferHeight;
            int bufferWidth;
            
            if (_model.State == EditorState.DefinitiveNumbers)
            {
                bufferHeight = height * 2 - 1;
                bufferWidth = width * 2 - 1;
            }
            else
            {
                (col, row) = _model.Numbers.Factorize();
                bufferHeight = height * row + height - 1;
                bufferWidth = width * col + width - 1;
            }
            
            var buffer = new string[bufferHeight, bufferWidth];

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var cell = cells.Get(x, y);
                    int y2, x2;

                    if (_model.State == EditorState.DefinitiveNumbers)
                    {
                        y2 = y * 2;
                        x2 = x * 2;
                        WriteDefiniteCell(cell, buffer, x2, y2, _model.Position.X == x && _model.Position.Y == y, _model.ShowErrors);
                    }
                    else
                    {
                        y2 = y * row + y;
                        x2 = x * col + x;
                        WriteAuxiliaryCell(cell, buffer, x2, y2, col, row, _model.Position.X == x && _model.Position.Y == y, _model.ShowErrors);
                    }

                    if (cell == null)
                    {
                        continue;
                    }

                    // Check for horizontal border
                    if (cells.Contains(x + 1, y) && 
                        cells.Get(x + 1, y) != null && 
                        cell.GridNumber != cells.Get(x + 1, y).GridNumber)
                    {
                        buffer.Set(x2 + col, y2, "|".Pastel(Color.OrangeRed));
                    }
            
                    // Check for vertical border
                    if (cells.Contains(x, y + 1) && 
                        cells.Get(x, y + 1) != null &&
                        cell.GridNumber != cells.Get(x, y + 1).GridNumber)
                    {
                        buffer.Set(x2, y2 + row, "-".Pastel(Color.OrangeRed));
                    }
                }
            }
            
            return buffer;
        }
        
        private static void WriteDefiniteCell(ICell cell, string[,] buffer, int xStart, int yStart, bool drawCursor, bool showErrors)
        {
            if (cell == null)
            {
                buffer.Set(xStart, yStart, " ");
                return;
            }

            var val = cell.Definite != 0 ? cell.Definite.ToString() : ".";
            if (cell.Error == true && showErrors)
            {
                val = val.Pastel(Color.White).PastelBg(drawCursor ? Color.DarkRed : Color.Red);
            }
            else if (drawCursor)
            {
                val = val.PastelBg(Color.DarkGray).Pastel(Color.Black);

            }

            buffer.Set(xStart, yStart, val);
        }
        
        private static void WriteAuxiliaryCell( ICell cell, string[,] buffer, int xStart, int yStart, int width, int height, bool drawCursor, bool showErrors)
        {
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var idx = y * width + x;
                    string val;

                    if (cell == null)
                    {
                        val = " ";
                    }
                    else if (cell.Definite != 0)
                    {
                        val = idx == cell.Definite - 1 ? cell.Definite.ToString() : " ";
                        val = cell.Error == true && showErrors ? 
                            val.Pastel(Color.White).PastelBg(drawCursor ? Color.DarkRed : Color.Red) : 
                            val.Pastel(Color.Black).PastelBg(drawCursor ? Color.LightGray : Color.LightYellow);
                    }
                    else
                    {
                        val = cell.Auxiliary[idx] != 0 ? cell.Auxiliary[idx].ToString() : ".";
                        if (drawCursor)
                        {
                            val = val.Pastel(Color.Black).PastelBg(Color.DarkGray);
                        }
                    }
 
                    buffer.Set(xStart + x, yStart + y, val);
                }
            }
        }
    }
}