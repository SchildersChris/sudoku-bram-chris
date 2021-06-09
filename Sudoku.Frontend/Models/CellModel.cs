using System.Drawing;
using Pastel;
using Sudoku.Domain.Composite.Interfaces;

namespace Sudoku.Frontend.Models
{
    public class CellModel
    {
        private Color? _foreground;
        private Color? _background;

        private bool _faulty;
        public bool Faulty
        {
            get => _faulty;
            set
            {
                _faulty = value;
                if (_faulty)
                {
                    _foreground = Color.White;
                    _background = Color.Red;
                }
                else
                {
                    _foreground = null;
                    _background = null;
                }
            }
        }

        public int GridNumber { get; }
        public int Definite { get; }
        public int[] Auxiliary { get; }

        public CellModel(ICell cell)
        {
            GridNumber = cell.GridNumber;
            Definite = cell.Definite;
            Auxiliary = cell.Auxiliary.Clone() as int[];
        }

        public override string ToString()
        {
            var val = Definite != 0 ? Definite.ToString() : ".";
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