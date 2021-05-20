using System;
using System.Collections.Generic;
using System.Drawing;
using Sudoku.Data.Extensions;
using Sudoku.Domain;

namespace Sudoku.Data.Factories
{
    public abstract class BaseRegularFactory : ISudokuFactory
    {
        private readonly GridBuilder _gridBuilder;

        protected BaseRegularFactory()
        {
            _gridBuilder = new GridBuilder();
        }

        public abstract IGame Create(IEnumerable<string> lines);
        
        protected void CreateSudoku(string line, int xStart, int yStart)
        {
            var length = (int) Math.Sqrt(line.Length);
            var (width, height) = length.Factorize();
            
            var sudoku = _gridBuilder.AddSudokuGrid(new Rectangle(0, 0, width, height));
            var subGrids = new List<GridBuilder>();
            for (var i = 0; i < length; i++)
            {
                subGrids.Add(sudoku.AddSubGrid());
            }

            for (var y = yStart; y < length + yStart; y++)
            {
                for (var x = xStart; x < length + xStart; x++)
                {
                    var number = int.Parse(line[y * length + x].ToString());
                    var yScale = y == 0 ? 0 : (y - yStart) / width;
                    var xScale = x == 0 ? 0 : (x - xStart) / height;

                    subGrids[yScale * width + xScale].AddCell(
                        new Point(x, y),
                        number
                    );
                }
            }
        }
    }
}