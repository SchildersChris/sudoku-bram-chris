using System.Collections.Generic;
using Sudoku.Domain.Enums;
using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Frontend.Models
{
    public class SudokuModel
    {
        private readonly IEnumerable<CellModel> _cells;
        
        public SudokuModel(IEnumerable<IReadonlyGrid> grids, EditorState editorState)
        {
            
        }

        void Move()
        {
            
        }
    }
}