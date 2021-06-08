using System.Drawing;
using Sudoku.Domain.Composite.Interfaces;

namespace Sudoku.Domain.Composite
{
    public class GridComposite : IGridComponent
    {
        private readonly IGridComponent[] _children;
        
        public GridComposite(IGridComponent[] children)
        {
            _children = children;
        }
        
        public virtual bool Check(Point point, int number)
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var c in _children)
            {
                if (!c.Check(point, number))
                {
                    return false;
                }
            }
            
            return true;
        }

        public virtual void Place(Point point, int number, bool isAuxiliary)
        {
            foreach (var c in _children)
            {
                c.Place(point, number, isAuxiliary);
            }
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