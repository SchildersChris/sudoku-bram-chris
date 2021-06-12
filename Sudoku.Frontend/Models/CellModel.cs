using System.Drawing;
using Pastel;
using Sudoku.Domain.Composite.Interfaces;

namespace Sudoku.Frontend.Models
{
    public class CellModel
    {
        private readonly ICell _cell;

        public int GridNumber => _cell.GridNumber;
        public int Definite => _cell.Definite;
        public int[] Auxiliary => _cell.Auxiliary;

        public CellModel(ICell cell)
        {
            _cell = cell;
        }

        public override string ToString()
        {
            var val = Definite != 0 ? Definite.ToString() : ".";
            if (_cell.Error == true)
            {
                val = val.Pastel(Color.White).PastelBg(Color.Red);
            }
            
            return val;
        }
    }
}