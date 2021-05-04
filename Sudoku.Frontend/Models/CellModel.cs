using System.Drawing;
using Pastel;

namespace Sudoku.Frontend.Models
{
    public class CellModel
    {
        private readonly Color? _foreground;
        private readonly Color? _background;
        private readonly uint? _number;

        public Point Point { get; }

        public CellModel(Point point, uint? number = null, Color? foreground = null, Color? background = null)
        {
            Point = point;
            
            _foreground = foreground;
            _background = background;
            _number = number;
        }
        
        public override string ToString()
        {
            var val = _number.HasValue ? _number.Value.ToString() : " ";
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