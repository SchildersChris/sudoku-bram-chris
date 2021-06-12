using System.Drawing;
using Sudoku.Common.Extensions;
using Sudoku.Domain.Composite.Interfaces;
using Sudoku.Domain.Enums;

namespace Sudoku.Frontend.Models
{
    public class SudokuModel
    {

        private readonly ICell[,] _cells;
        public CellModel[,] Cells { get; }
        public EditorState State { get; set; }
        public Point Position { get; private set; }
        
        public SudokuModel(ICell[,] cells, EditorState state)
        {
            _cells = cells;
            Cells = new CellModel[_cells.GetHeight(), _cells.GetWidth()];
            for (var y = 0; y < _cells.GetHeight(); y++)
            {
                for (var x = 0; x < _cells.GetWidth(); x++)
                {
                    var cell = _cells.Get(x, y);
                    if (cell != null)
                    {
                        Cells.Set(x, y, new CellModel(cell));
                    }
                }
            }
            
            State = state;
            Position = new Point(0, 0);
        }
        
        public void Move(Size size)
        {
            var newPos = Position + size;
            if (_cells.Contains(newPos) && _cells.Get(newPos) != null)
            {
                Position = newPos;
            }
        }
    }
}