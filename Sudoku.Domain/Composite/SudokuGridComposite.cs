using System.Collections.Generic;
using System.Drawing;
using Sudoku.Domain.Composite.Interfaces;

namespace Sudoku.Domain.Composite
{
    public class SudokuGridComposite : GridComposite
    {
        private readonly Rectangle _rect;
        public SudokuGridComposite(Rectangle rect, IEnumerable<IGridComponent> children) : base(children)
        {
            _rect = rect;
        }
        
        public override bool Place(Point point, int number, bool temporary)
        {
            return _rect.Contains(point) && base.Place(point, number, temporary);
        }
    }
}