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

        public override bool Place(Point point, int number, bool temporary)
        {
            if (temporary || !CheckInverted(point, number))
            {
                return base.Place(point, number, temporary);
            }
            
            base.Place(point, number, false);
            return false;
        }
    }
}