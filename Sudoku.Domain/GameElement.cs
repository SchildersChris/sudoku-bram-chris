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
        public bool?[,] Errors { get;  }
        public IGridComponent Grid { get; }

        public GameElement(int length, IGridComponent grid)
        {
            _currentState = new DefiniteNumberState(this);
            Cells = new ICell[length, length];
            Errors = new bool?[length, length];
            
            Grid = grid;
            Grid.Layout(Cells);
        }

        public void Place(Point point, int number)
        {
            if (!Cells.Contains(point))
            {
                throw new ArgumentException("Point must be within the board bounds", nameof(point));
            }

            _currentState.Place(point, number);
        }

        public bool Check(Point point, int number)
        {
            var cell = Cells.Get(point);
            if (cell == null)
            {
                return true;
            }

            for (var y = 0; y < Cells.GetHeight(); y++)
            {
                for (var x = 0; x < Cells.GetWidth(); x++)
                {
                    var c = Cells.Get(x, y);
                    if (c != null &&
                        (point.Y != y || point.X != x) &&
                        c.GridNumber == cell.GridNumber &&
                        c.Definite != 0 &&
                        c.Definite == cell.Definite)
                    {
                        return false;
                    }
                }
            }

            return Grid.Check(point, number);
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