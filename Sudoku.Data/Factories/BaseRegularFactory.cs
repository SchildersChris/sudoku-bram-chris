using System;
using System.Collections.Generic;
using System.Drawing;
using Sudoku.Common.Extensions;
using Sudoku.Domain;

namespace Sudoku.Data.Factories
{
    public abstract class BaseRegularFactory : ISudokuFactory
    {
        private readonly GridBuilder _gridBuilder;
        private int _subGridNumber;

        protected BaseRegularFactory()
        {
            _gridBuilder = new GridBuilder(0);
            _subGridNumber = 0;
        }

        protected abstract (int numbers, int length) Construct(IEnumerable<string> lines);

        protected void AddSudoku(string line, int xStart, int yStart)
        {
            var length = (int) Math.Sqrt(line.Length);
            var (width, height) = length.Factorize();

            var sudoku = _gridBuilder.AddSudokuGrid(new Rectangle(xStart, yStart, length, length));
            var subGrids = new List<GridBuilder>();
            for (var i = 0; i < length; i++)
            {
                subGrids.Add(sudoku.AddGrid(_subGridNumber++));
            }

            for (var y = 0; y < length; y++)
            {
                for (var x = 0; x < length; x++)
                {
                    var number = int.Parse(line[y * length + x].ToString());
                    var yScale = y == 0 ? 0 : y / height;
                    var xScale = x == 0 ? 0 : x / width;

                    subGrids[yScale * height + xScale].AddCell(
                        new Point(x + xStart, y + yStart),
                        number
                    );
                }
            }
        }

        public IGameElement Create(IEnumerable<string> lines)
        {
            var (numbers, length) = Construct(lines);
            return new GameElement(numbers, length, _gridBuilder.Build());
        }
    }
}