using System.Drawing;

namespace Sudoku.Domain.States
{
    public class AuxiliaryNumberState : IEditorState
    {
        private readonly Game _game;
        
        public AuxiliaryNumberState(Game game)
        {
            _game = game;
        }

        public bool Place(Point point, int number)
        {
            return _game.Sudoku.Place(point, number, true);
        }
    }
}