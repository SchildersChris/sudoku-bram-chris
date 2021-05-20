using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Sudoku.Domain;

namespace Sudoku.Data.Factories
{
    public class JigsawFactory : ISudokuFactory
    {
        private readonly GridBuilder _gridBuilder;

        public JigsawFactory()
        {
            _gridBuilder = new GridBuilder();
        }

        public IGame Create(IEnumerable<string> lines)
        {
            var line = lines.First();
            var parts = line.Split("=").Skip(1).ToArray();
            var length = (int) Math.Sqrt(parts.Length);

            var sudoku = _gridBuilder.AddSudokuGrid(new Rectangle(0, 0, length, length));
            var subGrids = new List<GridBuilder>();
            for (var i = 0; i < length; i++)
            {
                subGrids.Add(sudoku.AddSubGrid());
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

            return new Game(length, _gridBuilder.Build());
        }
    }
}