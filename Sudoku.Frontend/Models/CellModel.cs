using System.Drawing;
using Pastel;
using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Frontend.Models
{
    public class CellModel
    {
        private readonly Color? _foreground;
        private readonly Color? _background;

        public Point Point { get; }
        public int GridNumber { get; }
        public int? Number { get; }

        public CellModel(ICell cell, int gridNumber)
        {

            Point = cell.Point;
            GridNumber = gridNumber;
            Number = cell.Number;

            if (cell.Faulty)
            {
                _foreground = Color.Red;
            }

            if (cell.Temporary)
            {
                _background = Color.Yellow;
            }
        }
        
        public override string ToString()
        {
            var val = Number.HasValue ? Number.Value.ToString() : " ";
            if (_foreground.HasValue)
            {
                val = val.Pastel(_foreground.Value);
            }
            if (_background.HasValue)
            {
                val = val.PastelBg(_background.Value);
            }
            return val;
        }
    }
}