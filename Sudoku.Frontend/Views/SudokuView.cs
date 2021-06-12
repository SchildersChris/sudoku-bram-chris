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
            int bufferHeight, bufferWidth;
            
            if (_model.State == EditorState.AuxiliaryNumbers)
            {
                (col, row) = _model.Numbers.Factorize();
                bufferHeight = height * row;
                bufferWidth = width * col;
            }
            else
            {
                bufferHeight = height * 2 - 1;
                bufferWidth = width * 2 - 1;
            }
            
            var buffer = new string[bufferHeight, bufferWidth];

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var c = cells.Get(x, y);
                    int y2, x2;

                    if (_model.State == EditorState.DefinitiveNumbers)
                    {
                        y2 = y * 2;
                        x2 = x * 2;
                        WriteDefiniteCell(c, buffer, x2, y2, _model.Position.X == x && _model.Position.Y == y);
                    }
                    else
                    {
                        y2 = y * row;
                        x2 = x * col;
                        WriteAuxiliaryCell(c, buffer, x2, y2, col, row, _model.Position.X == x && _model.Position.Y == y);
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
                        buffer.Set(x2 + col, y2, "|".Pastel(Color.OrangeRed));
                    }
            
                    // Check for vertical border
                    if (cells.Contains(x, y + 1) && 
                        cells.Get(x, y + 1) != null &&
                        c.GridNumber != cells.Get(x, y + 1).GridNumber)
                    {
                        buffer.Set(x2, y2 + row, "-".Pastel(Color.OrangeRed));
                    }
                }
            }
            
            return buffer;
        }
        
        private static void WriteDefiniteCell(ICell cell, string[,] buffer, int xStart, int yStart, bool drawCursor)
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

            if (drawCursor)
            {
                val = val.PastelBg(Color.DarkGray).Pastel(Color.Black);
            }
            
            buffer.Set(xStart, yStart, val);
        }
        
        private static void WriteAuxiliaryCell(ICell cell, string[,] buffer, int xStart, int yStart, int w, int h, bool drawCursor)
        {
            for (var y = 0; y < h; y++)
            {
                for (var x = 0; x < w; x++)
                {
                    var idx = y * w + x;
                    string val;

                    if (cell == null)
                    {
                        val = " ";
                    }
                    else if (cell.Definite != 0)
                    {
                        val = idx == cell.Definite - 1 ? cell.Definite.ToString() : " ";
                        val = cell.Error == true ? 
                            val.Pastel(drawCursor ? Color.Black : Color.White).PastelBg(drawCursor ? Color.DarkRed : Color.Red) : 
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