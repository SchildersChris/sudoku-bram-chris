using System.Drawing;
using Sudoku.Domain.Composite.Interfaces;
using Sudoku.Domain.Enums;
using Sudoku.Domain.Solvers;
using Sudoku.Domain.States;

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