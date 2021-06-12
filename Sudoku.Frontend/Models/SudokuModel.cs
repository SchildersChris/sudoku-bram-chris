using System.Drawing;
using Sudoku.Common.Extensions;
using Sudoku.Domain.Composite.Interfaces;
using Sudoku.Domain.Enums;

namespace Sudoku.Frontend.Models
{
    public class SudokuModel
    {
        public ICell[,] Cells { get; }
        public EditorState State { get; set; }
        public Point Position { get; private set; }
        
        public SudokuModel(ICell[,] cells, EditorState state)
        {
            Cells = cells;
            State = state;
            Position = new Point(0, 0);
        }
        
        public void Move(Size size)
        {
            var newPos = Position + size;
            if (Cells.Contains(newPos) && Cells.Get(newPos) != null)
            {
                Position = newPos;
            }
        }
    }
}