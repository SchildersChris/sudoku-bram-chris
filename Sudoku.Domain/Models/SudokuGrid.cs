using System.Collections.Generic;
using System.Drawing;
using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Domain.Models
{
    public class SudokuGrid : Grid
    {
        private readonly Rectangle _rect;
        public SudokuGrid(Rectangle rect, IEnumerable<IGrid> children) : base(children)
        {
            _rect = rect;
        }
        
        public override bool Place(Point point, int number, bool temporary)
        {
            return _rect.Contains(point) && base.Place(point, number, temporary);
        }
    }
}