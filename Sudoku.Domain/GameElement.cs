using System;
using System.Drawing;
using Sudoku.Common.Extensions;
using Sudoku.Domain.Composite.Interfaces;
using Sudoku.Domain.Enums;
using Sudoku.Domain.States;
using Sudoku.Domain.Visitors;

namespace Sudoku.Domain
{
    public class GameElement : IGameElement
    {
        private IEditorState _currentState;
        public EditorState State => _currentState.State;
        public ICell[,] Cells { get; }
        public IGridComponent Grid { get; }

        public GameElement(int length, IGridComponent grid)
        {
            _currentState = new DefiniteNumberState(this);
            Cells = new ICell[length, length];
            
            Grid = grid;
            Grid.Layout(Cells);
        }

        public bool Place(Point point, int number)
        {
            if (!Cells.Contains(point))
            {
                throw new ArgumentException("Point must be within the board bounds", nameof(point));
            }
            
            return _currentState.Place(point, number);
        }

        public void ToggleState()
        {
            _currentState.SetState();
        }

        public void Accept(ISolverVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void SetState(IEditorState state)
        {
            _currentState = state;
        }
    }
}