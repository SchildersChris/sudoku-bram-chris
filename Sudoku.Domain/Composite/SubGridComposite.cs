using System.Collections.Generic;
using System.Drawing;
using Sudoku.Domain.Composite.Interfaces;

namespace Sudoku.Domain.Composite
{
    public class SubGridComposite : GridComposite
    {
        public SubGridComposite(IEnumerable<IGridComponent> children) : base(children)
        {
        }

        public override bool Place(Point point, int number, bool isAuxiliary)
        {
            if (isAuxiliary || !Check(point, number, false))
            {
                return base.Place(point, number, isAuxiliary);
            }
            
            base.Place(point, number, false);
            return false;
        }
    }
}