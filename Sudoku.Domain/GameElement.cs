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
        public IGridComponent Grid { get; }

        private readonly ICell[,] _cells;

        public ICell[,] Cells
        {
            get
            {
                Grid.Layout(_cells);
                return _cells;
            }
        }


        public GameElement(int length, IGridComponent grid)
        {
            Grid = grid;
            _cells = new ICell[length, length];
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