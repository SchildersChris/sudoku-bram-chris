using System.Drawing;
using Pastel;
using Sudoku.Common.Extensions;
using Sudoku.Frontend.Models;

namespace Sudoku.Frontend.Views
{
    public class AuxiliarySudokuView : SudokuView
    {
        public AuxiliarySudokuView(SudokuModel model) : base(model)
        { }
        
        protected override string[,] CreateSudoku()
        {
            var cells = Model.Cells;
            var height = cells.GetHeight();
            var width = cells.GetWidth();

            var (col, row) = Model.Numbers.Factorize();
            var bufferHeight = height * row + height - 1;
            var bufferWidth = width * col + width - 1;
            
            var buffer = new string[bufferHeight, bufferWidth];

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var cell = cells.Get(x, y);

                    var y2 = y * row + y;
                    var x2 = x * col + x;
                    var drawCursor = Model.Position.X == x && Model.Position.Y == y;

                    for (var yy = 0; yy < row; yy++)
                    {
                        for (var xx = 0; xx < col; xx++)
                        {
                            var idx = yy * col + xx;
                            string val;

                            if (cell == null)
                            {
                                val = " ";
                            }
                            else if (cell.Definite != 0)
                            {
                                val = idx == cell.Definite - 1 ? cell.Definite.ToString() : " ";
                                val = cell.Error == true && Model.ShowErrors ? 
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
 
                            buffer.Set(x2 + xx, y2 + yy, val);
                        }
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
    }
}