using System.Drawing;
using Sudoku.Domain.Composite.Interfaces;
using Sudoku.Domain.Enums;
using Sudoku.Domain.Solvers;

namespace Sudoku.Domain
{
    public interface IGameElement
    {
        EditorState State { get; }
        ICell[,] Cells { get; }
        bool Place(Point point, int number);
        void ToggleState();
        void Accept(ISolverVisitor visitor);
    }
}