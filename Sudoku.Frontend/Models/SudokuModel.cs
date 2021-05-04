using System;
using System.Collections.Generic;

namespace Sudoku.Frontend.Models
{
    /// <summary>
    /// This class represents a model which contains all view related information.
    /// With the purpose of being able to store view state.
    /// </summary>
    public class SudokuModel
    {
        public IEnumerable<CellModel> Cells { get; set; }
        
        public SudokuModel()
        {
            Cells = Array.Empty<CellModel>();
        }
    }
}