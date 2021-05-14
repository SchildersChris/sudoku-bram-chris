using System.Drawing;

namespace Sudoku.Domain.States
{
    public interface IEditorState
    {
        public bool Place(Point point, int number);
    }
}