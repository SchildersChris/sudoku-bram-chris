using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Sudoku.Domain.Models;

namespace Sudoku.Data.Factories
{
    public class JigsawFactory : ISudokuFactory
    {
        private readonly GridBuilder _gridBuilder;

        public JigsawFactory()
        {
            _gridBuilder = new GridBuilder();
        }

        public SudokuModel Create(IEnumerable<string> lines)
        {
            var line = lines.First();
            var parts = line.Split("=").Skip(1).ToArray();
            var length = (int) Math.Sqrt(parts.Length);

            var subGrids = new List<GridBuilder>();
            for (var i = 0; i < length; i++)
            {
                subGrids.Add(_gridBuilder.AddSubGrid());
            }

            for (var i = 0; i < parts.Length; i++)
            {
                var part = parts[i];
                var cell = part.Split("J");

                var number = int.Parse(cell[0]);
                subGrids[int.Parse(cell[0])].AddCell(
                    new Point(i % length, i / length),
                    number
                );
            }

            return new SudokuModel(length, length, _gridBuilder.Build());
        }
    }
}