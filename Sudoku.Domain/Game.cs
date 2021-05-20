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
        
        public IGrid Sudoku { get; }
        public ICell[,] Cells { get; }
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

        public Game(int length, IGrid sudoku)
        {
            Sudoku = sudoku;
            Cells = new ICell[length, length];
            
            Sudoku.Layout(Cells);
        }

        public bool Place(Point point, int number)
        {
            return _currentState.Place(point, number);
        }
    }
}