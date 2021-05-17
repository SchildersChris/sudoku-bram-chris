using System;
using System.Drawing;
using Sudoku.Domain.Enums;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Solvers;
using Sudoku.Domain.States;

namespace Sudoku.Domain
{
    public class Game : IGame
    {
        private readonly ISolver _solver;
        public SudokuModel Sudoku { get; }
        public ICell[,] Cells => Sudoku.Cells;

        private IEditorState _currentState;
        private EditorState _state;

        public EditorState State
        {
            get => _state;
            set
            {
                _state = value;
                _currentState = _state switch
                {
                    EditorState.DefinitiveNumbers => new DefiniteNumberState(this),
                    EditorState.AuxiliaryNumbers => new AuxiliaryNumberState(this),
                    _ => throw new ArgumentOutOfRangeException(nameof(State), _state, "State is not valid.")
                };
            }
        }

        public Game(SudokuModel sudoku)
        {
            Sudoku = sudoku;
            _solver = new BackTrackingSolver();
        }

        public void Solve()
        {
            Sudoku.Accept(_solver);
        }

        public bool Place(Point point, int number)
        {
            return _currentState.Place(point, number);
        }
    }
}