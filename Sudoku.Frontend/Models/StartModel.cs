namespace Sudoku.Frontend.Models
{
    public class StartModel
    {
        public bool SimpleDisplay { get; set; }
        public string SudokuPath { get; set; }
        public string Error { get; set; }

        public StartModel()
        {
            SimpleDisplay = true;
        }
    }
}