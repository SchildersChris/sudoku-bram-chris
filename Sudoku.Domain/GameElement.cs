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
        public int Numbers { get; }

        public GameElement(int numbers, int length, IGridComponent grid)
        {
            _currentState = new DefiniteNumberState(this);
            Cells = new ICell[length, length];
            
            Grid = grid;
            Grid.Layout(Cells);
            Numbers = numbers;
        }

        public void Place(Point point, int number)
        {
            if (Numbers < number || number < 0)
            {
                throw new ArgumentException($"The number must be between 1 and {Numbers}");
            }
            
            if (!Cells.Contains(point))
            {
                throw new ArgumentException("Position must be within the board bounds", nameof(point));
            }

            _currentState.Place(point, number);
        }

        public bool Validate(Point point, int number)
        {
            var cell = Cells.Get(point);
            if (cell == null)
            {
                return true;
            }
            
            return Grid.Contains(point, number, cell.GridNumber) && Grid.Check(point, number);
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