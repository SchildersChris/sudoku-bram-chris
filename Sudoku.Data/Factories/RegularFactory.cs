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
                var pairs = new List<(int, int)>();
                var len = (int)Math.Ceiling(gridLen);
                for (var i = 1; i <= len; i++)
                {
                    for (var j = len; j >= 0; j--)
                    {
                        if (j * i == length)
                        {
                            pairs.Add((j, i));
                        }  
                    }
                }

                var min = int.MaxValue;
                foreach (var (j, i) in pairs)
                {
                    var sum = j + i;
                    if (sum >= min) 
                        continue;
                    
                    min = sum;
                    width = j;
                    height = i;
                }
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
                    var number = int.Parse(line[y * length + x].ToString());
                    var yScale = y == 0 ? 0 : y / width;
                    var xScale = x == 0 ? 0 : x / height;

                    subGrids[yScale * width + xScale].AddCell(
                        new Point(x, y),
                        number
                    );
                }
            }

            return new SudokuModel(length, length, _gridBuilder.Build());
        }
    }
}