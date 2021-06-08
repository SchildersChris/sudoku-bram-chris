using System.Drawing;
using Sudoku.Domain.Enums;

namespace Sudoku.Domain.States
{
    public class DefiniteNumberState : IEditorState
    {
        private readonly GameElement _game;
        public EditorState State { get; }

        public DefiniteNumberState(GameElement game)
        {
            _game = game;
            State = EditorState.DefinitiveNumbers;
        }

        public void Place(Point point, int number)
        {
            _game.Grid.Place(point, number, false);
        }

        public void SetState()
        {
            _game.SetState(new AuxiliaryNumberState(_game));
        }
    }
}