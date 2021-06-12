using System.Drawing;
using Sudoku.Domain.Composite.Interfaces;
using Sudoku.Domain.Enums;
using Sudoku.Domain.Visitors;

namespace Sudoku.Domain
{
    public interface IGameElement
    {
        int Numbers { get; }
        EditorState State { get; }
        ICell[,] Cells { get; }
        void Place(Point point, int number);
        void ToggleState();
        void Accept(ISolverVisitor visitor);
    }
}