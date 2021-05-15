using System;
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

        public int Count()
        {
            return _children.Sum(c => c.Count());
        }

        public bool Check(int number)
        {
            return _children.Any(c => c.Check(number));
        }
        
        public bool Check(Point point, int number)
        {
            return _children.Any(c => c.Check(point, number));
        }

        public bool Place(Point point, int number, bool temporary)
        {
            var len = (int)Math.Sqrt(Count());
            
            var placeResult = _children.All(c => c.Place(point, number, temporary));
            if (!temporary && _children.Any(c => c.Check(number)))
            {
                return false;
            }
            
            return placeResult;
        }

        public void Layout(ICell[,] cells)
        {
            foreach (var child in _children)
            {
                child.Layout(cells);
            }
        }
    }
}