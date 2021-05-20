using System.Drawing;
using Pastel;
using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Frontend.Models
{
    public class CellModel
    {
        private readonly Color? _foreground;
        private readonly Color? _background;
        public int GridNumber { get; }
        public int Definite { get; }
        public int[] Auxiliary { get; }

        public CellModel(ICell cell)
        {
            GridNumber = cell.GridNumber;
            Definite = cell.Definite;
            Auxiliary = cell.Auxiliary.Clone() as int[];

            if (!cell.Faulty)
            {
                return;
            }

            _foreground = Color.White;
            _background = Color.Red;
        }

        public override string ToString()
        {
            var val = Definite != 0 ? Definite.ToString() : " ";
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