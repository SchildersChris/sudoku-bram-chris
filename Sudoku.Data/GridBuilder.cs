using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Data
{
    public class GridBuilder
    {
        private readonly int _gridNumber;
        private readonly Func<IEnumerable<IGrid>, IGrid> _builder;
        private readonly List<Func<int, IGrid>> _leaves;

        private readonly List<GridBuilder> _builders;

        public GridBuilder()
        {
            _gridNumber = 0;
            _builder = children => new Grid(children);
            _leaves = new List<Func<int, IGrid>>();
            
            _builders = new List<GridBuilder>();
        }
    
        private GridBuilder(int gridNumber, Func<IEnumerable<IGrid>, IGrid> builder)
        {
            _gridNumber = gridNumber;
            _builder = builder;
            _leaves = new List<Func<int, IGrid>>();
            
            _builders = new List<GridBuilder>();
        }

        public GridBuilder AddSudokuGrid(Rectangle rect)
        {
            var builder = new GridBuilder(
                _builders.Count + 1, 
                children => new SudokuGrid(rect, children)
            );
            _builders.Add(builder);
            return builder;
        }
        
        public GridBuilder AddSubGrid()
        {
            var builder = new GridBuilder(
                _builders.Count + 1,
                children => new SubGrid(children)
            );
            _builders.Add(builder);
            return builder;
        }

        public void AddCell(Point point, int number)
        {
            _leaves.Add(gridNumber => new Cell(point, gridNumber, _leaves.Count, number));
        }

        public IGrid Build()
        {
            var grids = new List<IGrid>(_builders.Select(b => b.Build()));
            grids.AddRange(_leaves.Select(leaf => leaf(_gridNumber)));

            return _builder(grids);
        }
    }
}