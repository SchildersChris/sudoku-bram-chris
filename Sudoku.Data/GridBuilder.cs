using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Data
{
    public class GridBuilder
    {
        private readonly List<GridBuilder> _builders;
        private readonly List<IGrid> _grids;
        
        public GridBuilder()
        {
            _builders = new List<GridBuilder>();
            _grids = new List<IGrid>();
        }
        
        public GridBuilder AddSubGrid()
        {
            var builder = new GridBuilder();
            _builders.Add(builder);
            
            return builder;
        }
        
        public void AddCell(Point point, int gridNumber, int? number)
        {
            _grids.Add(new Cell(point, gridNumber, number));
        }
        
        public IGrid Build()
        {
            return new SubGrid(_builders.Select(b => b.Build()).Concat(_grids));
        }
    }
}