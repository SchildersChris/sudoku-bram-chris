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
        private readonly IGame _game;
        private readonly ISolver _solver;

        public SudokuController(IGame game, bool simpleDisplay)
        {
            game.State = simpleDisplay ? EditorState.DefinitiveNumbers : EditorState.AuxiliaryNumbers;

            _model = new SudokuModel(game.Cells)
            {
                State = game.State
            };
            _view = new SudokuView(_model);
            _game = game;
            _solver = new BackTrackingSolver();
        }

        public void Update(ConsoleKey key)
        {
            switch (key)
            {
                // Todo: Do action
            }

            // Todo: Update model

            _view.Update();
        }
    }
}