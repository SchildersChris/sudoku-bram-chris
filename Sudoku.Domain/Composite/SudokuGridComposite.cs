using System.Drawing;
using Sudoku.Domain.Composite.Interfaces;

namespace Sudoku.Domain.Composite
{
    public class SudokuGridComposite : GridComposite
    {
        private readonly Rectangle _rect;
        
        public SudokuGridComposite(Rectangle rect, IGridComponent[] children) : base(children)
        {
            _rect = rect;
        }
        
        public override bool Contains(Point point, int number, int gridNumber)
        {
            return !_rect.Contains(point) || base.Contains(point, number, gridNumber);
        }
        
        public override bool Check(Point point, int number)
        {
            return !_rect.Contains(point) || base.Check(point, number);
        }
        
        public override void Place(Point point, int number, bool isAuxiliary)
        {
            if (_rect.Contains(point))
            {
                base.Place(point, number, isAuxiliary);
            }
        }
    }
}