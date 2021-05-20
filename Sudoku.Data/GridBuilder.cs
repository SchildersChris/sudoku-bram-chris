using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Data
{
    public class GridBuilder
    {
        private readonly int _childIndex;
        private readonly List<GridBuilder> _builders;
        private readonly List<(Point, int)> _leaves;

        public GridBuilder()
        {
            _childIndex = 0;
            _builders = new List<GridBuilder>();
            _leaves = new List<(Point, int)>();
        }

        private GridBuilder(int childIndex = 0)
        {
            _childIndex = childIndex;
            _builders = new List<GridBuilder>();
            _leaves = new List<(Point, int)>();
        }

        public GridBuilder AddSubGrid()
        {
            var builder = new GridBuilder(_builders.Count + 1);
            _builders.Add(builder);

            return builder;
        }

        public void AddCell(Point point, int number)
        {
            _leaves.Add((point, number));
        }

        public IGrid Build()
        {
            return new SubGrid(_builders.Select(b => b.Build())
                .Concat(_leaves.Select(leaf => new Cell(leaf.Item1, _childIndex, _leaves.Count, leaf.Item2))));
        }
    }
}