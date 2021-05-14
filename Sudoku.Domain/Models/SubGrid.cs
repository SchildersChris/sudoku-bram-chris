using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Domain.Models
{
    public class SubGrid : IGrid
    {
        private readonly IEnumerable<IGrid> _children;

        public SubGrid(IEnumerable<IGrid> children)
        {
            _children = children;
        }

        public bool Check(Point point, int number)
        {
            return _children.Any(c => c.Check(point, number));
        }

        public bool Place(Point point, int number, bool temporary)
        {
            return _children.Any(c => c.Place(point, number, temporary));
        }

        public IEnumerable<ICell> GetCells()
        {
            return _children.SelectMany(c => c.GetCells());
        }
    }
}