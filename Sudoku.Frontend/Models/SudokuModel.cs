using Sudoku.Domain.Composite.Interfaces;
using Sudoku.Domain.Enums;

namespace Sudoku.Frontend.Models
{
    public class SudokuModel
    {
        public CellModel[,] Cells { get; }
        public EditorState State { get; set; }

        public SudokuModel(ICell[,] cells)
        {
            var width = cells.GetLength(1);
            var height = cells.GetLength(0);
            Cells = new CellModel[height, width];

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var cell = cells[y, x];
                    if (cell != null)
                    {
                        Cells[y, x] = new CellModel(cell);
                    }
                }
            }

            State = EditorState.DefinitiveNumbers;
        }

        void Move()
        {
        }
    }
}