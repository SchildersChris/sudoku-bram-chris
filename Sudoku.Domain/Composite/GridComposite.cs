using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Sudoku.Domain.Composite.Interfaces;

namespace Sudoku.Domain.Composite
{
    public class GridComposite : IGridComponent
    {
        private readonly IEnumerable<IGridComponent> _children;
        public GridComposite(IEnumerable<IGridComponent> children)
        {
            _children = children;
        }
        
        public virtual bool Check(int number)
        {
            return _children.Any(c => c.Check(number));
        }
        
        public virtual bool Place(Point point, int number, bool temporary)
        {
            return _children.Any(c => c.Place(point, number, temporary));
        }
        
        public virtual void Layout(ICell[,] cells)
        {
            foreach (var c in _children)
            {
                c.Layout(cells);
            }
        }
    }
}