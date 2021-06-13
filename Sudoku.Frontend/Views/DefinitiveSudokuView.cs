using System.Drawing;
using Pastel;
using Sudoku.Common.Extensions;
using Sudoku.Frontend.Models;

namespace Sudoku.Frontend.Views
{
    public class DefinitiveSudokuView : SudokuView
    {
        public DefinitiveSudokuView(SudokuModel model) : base(model)
        { }
        
        protected override string[,] CreateSudoku()
        {
            var cells = Model.Cells;
            var height = cells.GetHeight();
            var width = cells.GetWidth();
            
            var bufferHeight = height * 2 - 1;
            var bufferWidth = width * 2 - 1;
            
            var buffer = new string[bufferHeight, bufferWidth];

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var cell = cells.Get(x, y);
                    
                    var y2 = y * 2;
                    var x2 = x * 2;
                    var drawCursor = Model.Position.X == x && Model.Position.Y == y;
                    
                    if (cell == null)
                    {
                        buffer.Set(x2, y2, " ");
                        continue;
                    }

                    var val = cell.Definite != 0 ? cell.Definite.ToString() : ".";
                    if (cell.Error == true && Model.ShowErrors)
                    {
                        val = val.Pastel(Color.White).PastelBg(drawCursor ? Color.DarkRed : Color.Red);
                    }
                    else if (drawCursor)
                    {
                        val = val.PastelBg(Color.DarkGray).Pastel(Color.Black);
                    }

                    buffer.Set(x2, y2, val); 

                    // Check for horizontal border
                    if (cells.Contains(x + 1, y) && 
                        cells.Get(x + 1, y) != null && 
                        cell.GridNumber != cells.Get(x + 1, y).GridNumber)
                    {
                        buffer.Set(x2 + 1, y2, "|".Pastel(Color.OrangeRed));
                    }
            
                    // Check for vertical border
                    if (cells.Contains(x, y + 1) && 
                        cells.Get(x, y + 1) != null &&
                        cell.GridNumber != cells.Get(x, y + 1).GridNumber)
                    {
                        buffer.Set(x2, y2 + 1, "-".Pastel(Color.OrangeRed));
                    }
                }
            }
            
            return buffer;
        }
    }
}