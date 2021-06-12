using System;
using System.Drawing;
using Sudoku.Domain;
using Sudoku.Domain.Enums;
using Sudoku.Domain.Visitors;
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
        private readonly ISolverVisitor _solver2;

        public SudokuController(IGameElement game, bool simpleDisplay)
        {
            if (simpleDisplay && game.State != EditorState.DefinitiveNumbers)
            {
                game.ToggleState();
            }

            _model = new SudokuModel(game.Cells, game.State)
            {
                State = game.State
            };
            _view = new SudokuView(_model);
            
            _game = game;
            _solver = new BoxLogicSolverVisitor();
            _solver2 = new BackTrackingSolverVisitor();
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
                    _game.Accept(_solver2);
                    break;
                }
                case ConsoleKey.UpArrow:
                    _model.Move(new Size(0, -1));
                    break;
                case ConsoleKey.DownArrow:
                    _model.Move(new Size(0, 1));
                    break;
                case ConsoleKey.LeftArrow:
                    _model.Move(new Size(-1, 0));
                    break;
                case ConsoleKey.RightArrow:
                    _model.Move(new Size(1, 0));
                    break;
                case ConsoleKey.D1:
                case ConsoleKey.D2:
                case ConsoleKey.D3:
                case ConsoleKey.D4:
                case ConsoleKey.D5:
                case ConsoleKey.D6:
                case ConsoleKey.D7:
                case ConsoleKey.D8:
                case ConsoleKey.D9:
                {
                    _game.Place(_model.Position, (int) key - 48);
                    break;
                }
                // Todo: Do actions 
            }
            
            _view.Update();
        }
    }
}