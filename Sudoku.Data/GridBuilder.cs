using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Sudoku.Domain.Composite;
using Sudoku.Domain.Composite.Interfaces;

namespace Sudoku.Data
{
    public class GridBuilder
    {
        private readonly int _gridNumber;
        private readonly Func<IGridComponent[], IGridComponent> _builder;
        private readonly List<Func<int, IGridComponent>> _leaves;

        private readonly List<GridBuilder> _builders;

        public GridBuilder(int gridNumber)
        {
            _gridNumber = gridNumber;
            _builder = children => new GridComposite(children);
            _leaves = new List<Func<int, IGridComponent>>();
            
            _builders = new List<GridBuilder>();
        }
    
        private GridBuilder(int gridNumber, Func<IGridComponent[], IGridComponent> builder)
        {
            _gridNumber = gridNumber;
            _builder = builder;
            _leaves = new List<Func<int, IGridComponent>>();
            
            _builders = new List<GridBuilder>();
        }

        public GridBuilder AddSudokuGrid(Rectangle rect)
        {
            var builder = new GridBuilder(
                _builders.Count + 1, 
                children => new SudokuGridComposite(rect, children)
            );
            _builders.Add(builder);
            return builder;
        }
        
        public GridBuilder AddGrid(int gridNumber)
        {
            var builder = new GridBuilder(
                gridNumber,
                children => new GridComposite(children)
            );
            _builders.Add(builder);
            return builder;
        }

        public void AddCell(Point point, int number)
        {
            _leaves.Add(gridNumber => new CellLeaf(point, gridNumber, _leaves.Count, number));
        }

        public IGridComponent Build()
        {
            var grids = new List<IGridComponent>(_builders.Select(b => b.Build()));
            grids.AddRange(_leaves.Select(leaf => leaf(_gridNumber)));

            return _builder(grids.ToArray());
        }
    }
}