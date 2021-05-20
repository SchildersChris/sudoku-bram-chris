using System.Collections.Generic;
using System.Drawing;
using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Domain.Models
{
    public class SubGrid : Grid
    {
        public SubGrid(IEnumerable<IGrid> children) : base(children)
        {
        }
        
        public override bool Place(Point point, int number, bool temporary)
        {
            var placeResult = base.Place(point, number, temporary);
            if (!temporary && Check(number))
            {
                return false;
            }

            return placeResult;
        }
    }
}