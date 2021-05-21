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
        
        public override void Place(Point point, int number, bool temporary)
        {
            if (temporary || !Contains(number))
            {
                base.Place(point, number, temporary);
                return;
            }
            base.Place(point, number, false);
        }
    }
}