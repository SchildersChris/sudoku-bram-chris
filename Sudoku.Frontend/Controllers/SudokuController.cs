using System;
using Sudoku.Domain;
using Sudoku.Domain.Enums;
using Sudoku.Domain.Solvers;
using Sudoku.Frontend.Models;
using Sudoku.Frontend.Views;

namespace Sudoku.Frontend.Controllers
{
    public class SudokuController : IController
    {
        private readonly SudokuView _view;
        private readonly SudokuModel _model;
        private readonly IGameElement _game;
        private readonly ISolverVisitor _solver;

        public SudokuController(IGameElement game, bool simpleDisplay)
        {
            if (simpleDisplay && game.State != EditorState.DefinitiveNumbers)
            {
                game.ToggleState();
            }

            _model = new SudokuModel(game.Cells)
            {
                State = game.State
            };
            _view = new SudokuView(_model);
            _game = game;
            _solver = new BackTrackingSolverVisitor();
        }

        public void Update(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Spacebar:
                {
                    _game.ToggleState();
                    _model.State = _game.State;
                    break;
                }
                case ConsoleKey.S:
                {
                    _game.Accept(_solver);
                    break;
                }
                // Todo: Do actions 
            }

            // Todo: Update model

            _view.Update();
        }
    }
}