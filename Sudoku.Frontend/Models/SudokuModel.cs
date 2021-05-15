using Sudoku.Domain.Enums;
using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Frontend.Models
{
    public class SudokuModel
    {
        public CellModel[,] Cells { get; }
        
        public SudokuModel(ICell[,] cells, EditorState editorState)
        {
            
        }

        void Move()
        {
            
        }
    }
}