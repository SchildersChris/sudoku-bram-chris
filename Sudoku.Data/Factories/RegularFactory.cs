using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Sudoku.Domain.Models;

namespace Sudoku.Data.Factories
{
    public class RegularFactory : ISudokuFactory
    {
        private readonly GridBuilder _gridBuilder;

        public RegularFactory()
        {
            _gridBuilder = new GridBuilder();
        }

        public SudokuModel Create(IEnumerable<string> lines)
        {
            var line = lines.First();
            var length = (int) Math.Sqrt(line.Length);
            var gridLen = Math.Sqrt(length);

            var width = (int) gridLen;
            var height = (int) gridLen;

            if (gridLen % 1 != 0)
            {
                // Todo: This case we do not have a square.
                // Factorize the "length" for width and height 
            }

            var subGrids = new List<GridBuilder>();
            for (var i = 0; i < length; i++)
            {
                subGrids.Add(_gridBuilder.AddSubGrid());
            }

            for (var y = 0; y < length; y++)
            {
                for (var x = 0; x < length; x++)
                {
                    var yScale = y == 0 ? 0 : y / height;
                    var xScale = x == 0 ? 0 : x / width;
                    subGrids[yScale * width + xScale].AddCell(
                        new Point(x, y),
                        int.Parse(line[y * length + x].ToString())
                    );
                }
            }

            return new SudokuModel(length, length, _gridBuilder.Build());
        }
    }
}