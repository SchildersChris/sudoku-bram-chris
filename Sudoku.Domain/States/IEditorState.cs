using System.Drawing;
using Sudoku.Domain.Enums;

namespace Sudoku.Domain.States
{
    public interface IEditorState
    {
        public EditorState State { get; }
        public bool Place(Point point, int number);
        public void SetState();
    }
}