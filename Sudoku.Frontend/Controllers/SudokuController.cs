using System;
using System.Drawing;
using Sudoku.Frontend.Models;
using Sudoku.Frontend.Views;

namespace Sudoku.Frontend.Controllers
{
    public class SudokuController
    {
        private readonly SudokuView _view;
        private readonly SudokuModel _model;
        
        public SudokuController()
        {
            _model = new SudokuModel();
            _view = new SudokuView(_model);

            // Todo: Remove
            _model.Cells = new[]
            {
                new CellModel(new Point(0, 0), null, Color.Bisque),
                new CellModel(new Point(1, 0), null, Color.Bisque),
                new CellModel(new Point(2, 0), null, Color.Bisque),
                new CellModel(new Point(3, 0), null, Color.Bisque),
                new CellModel(new Point(0, 1), null, Color.Bisque),
                new CellModel(new Point(1, 1), null, Color.Bisque),
                new CellModel(new Point(2, 1), null, Color.Bisque),
                new CellModel(new Point(3, 1), null, Color.Bisque),
                new CellModel(new Point(0, 2), null, Color.Bisque),
                new CellModel(new Point(1, 2), null, Color.Bisque),
                new CellModel(new Point(2, 2), null, Color.Bisque),
                new CellModel(new Point(3, 2), null, Color.Bisque),
                new CellModel(new Point(0, 3), null, Color.Purple),
                new CellModel(new Point(1, 3), null, Color.Purple),
                new CellModel(new Point(2, 3), null, Color.Purple),
                new CellModel(new Point(3, 3), null, Color.Purple),
            };
        }

        public void Update(ConsoleKey key)
        {
            
            
            
            _view.Update();
        }
    }
}