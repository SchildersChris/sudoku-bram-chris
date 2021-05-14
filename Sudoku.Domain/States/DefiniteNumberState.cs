using System.Drawing;

namespace Sudoku.Domain.States
{
    public class DefiniteNumberState : IEditorState
    {
        private readonly Game _game;
        
        public DefiniteNumberState(Game game)
        {
            _game = game;
        }

        public bool Place(Point point, int number)
        {
            return _game.Sudoku.Place(point, number, false);
        }
    }
}