using System.Drawing;
using Sudoku.Domain.Enums;

namespace Sudoku.Domain.States
{
    public interface IEditorState
    {
        public EditorState State { get; }
        public void Place(Point point, int number);
        public void SetState();
    }
}