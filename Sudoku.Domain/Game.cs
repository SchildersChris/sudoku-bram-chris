using System;
using System.Drawing;
using Sudoku.Domain.Enums;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.States;

namespace Sudoku.Domain
{
    public class Game : IGame
    {
        private IEditorState _currentState;
        private EditorState _state;
        
        public IGrid Grid { get; }

        private readonly ICell[,] _cells;
        public ICell[,] Cells
        {
            get
            {
                Grid.Layout(_cells);
                return _cells;
            }
        }

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

        public Game(int length, IGrid grid)
        {
            Grid = grid;
            _cells = new ICell[length, length];
        }

        public bool Place(Point point, int number)
        {
            return _currentState.Place(point, number);
        }
    }
}