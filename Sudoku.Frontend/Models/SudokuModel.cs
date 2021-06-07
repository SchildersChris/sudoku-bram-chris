using System.Drawing;
using Sudoku.Domain.Composite.Interfaces;
using Sudoku.Domain.Enums;

namespace Sudoku.Frontend.Models
{
    public class SudokuModel
    {
        private readonly Point _position;
        private readonly ICell[,] _cells;
        public CellModel[,] Cells { get; }
        public EditorState State { get; set; }

        public SudokuModel(ICell[,] cells)
        {
            _cells = cells;
            
            var width = _cells.GetLength(1);
            var height = _cells.GetLength(0);
            Cells = new CellModel[height, width];
            
            UpdateCells();
            
            State = EditorState.DefinitiveNumbers;
        }

        public void UpdateCells()
        {
            var width = _cells.GetLength(1);
            var height = _cells.GetLength(0);

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var cell = _cells[y, x];
                    if (cell != null)
                    {
                        Cells[y, x] = new CellModel(cell);
                    }
                }
            }
        }

        void Move()
        {
            // Todo: move _position
        }

        void Place()
        {
            Cells[_position.Y, _position.X] = new CellModel(_cells[_position.X, _position.Y]);
        }
    }
}