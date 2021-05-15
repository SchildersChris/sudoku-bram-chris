using System;
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
        public int? Number { get; }
        public int?[] Temporary { get; }

        public CellModel(ICell cell)
        {
            Number = cell.Number;
            Temporary = cell.Temporary.Clone() as int?[];

            if (!cell.Faulty)
            {
                return;
            }
            
            _foreground = Color.White;
            _background = Color.Red;
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