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
        private SudokuView _view;
        private readonly SudokuModel _model;
        private readonly IGameElement _game;
        private readonly ISolverVisitor _solver;
        private readonly ISolverVisitor _solver2;

        public SudokuController(IGameElement game, bool simpleDisplay)
        {
            _model = new SudokuModel(game.Numbers, game.Cells, game.State);

            if (simpleDisplay)
            {
                _view = new DefinitiveSudokuView(_model);
            }
            else
            {
                game.ToggleState();
                _view = new AuxiliarySudokuView(_model);
            }
            
            _game = game;
            _solver = new BoxLogicSolverVisitor();
            _solver2 = new BackTrackingSolverVisitor();
            
            _view.Update();
        }

        public void Update(ConsoleKey key)
        {
            _model.Error = null;
            
            switch (key)
            {
                case ConsoleKey.Spacebar: // Toggle display mode
                {
                    _game.ToggleState();
                    _model.State = _game.State;
                    
                    _view = _model.State == EditorState.DefinitiveNumbers ? 
                        new DefinitiveSudokuView(_model) : 
                        new AuxiliarySudokuView(_model);

                    break;
                }
                case ConsoleKey.S: // Solve puzzle
                {
                    _game.Accept(_solver);
                    _game.Accept(_solver2);
                    break;
                }
                case ConsoleKey.C: // Show errors on screen
                {
                    _model.ShowErrors = !_model.ShowErrors;
                    break;
                }
                // Navigate sudoku board
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
                // Placing number
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
                    try
                    {
                        _game.Place(_model.Position, (int) key - (int) ConsoleKey.D0);
                    }
                    catch (Exception ex)
                    {
                        _model.Error = ex.Message;
                    }
                    break;
                }
                default: return;
            }
            
            _view.Update();
        }
    }
}