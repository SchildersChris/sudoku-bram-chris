using System;
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
            
            if (!_rect.Contains(point))
            {
                return true;
            }
            // Console.WriteLine($"Rect: { _rect.ToString() }, Point: {point.ToString()}, Number: {number}, GridNumber: {gridNumber}" );

            return base.Contains(point, number, gridNumber);
        }
        
        public override bool Check(Point point, int number)
        {
            if (!_rect.Contains(point))
            {
                return true;
            }
            
            return base.Check(point, number);
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