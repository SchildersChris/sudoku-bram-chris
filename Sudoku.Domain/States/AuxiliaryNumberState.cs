using System.Drawing;
using Sudoku.Domain.Enums;

namespace Sudoku.Domain.States
{
    public class AuxiliaryNumberState : IEditorState
    {
        private readonly GameElement _game;
        public EditorState State { get; }

        public AuxiliaryNumberState(GameElement game)
        {
            _game = game;
            State = EditorState.AuxiliaryNumbers;
        }

        public bool Place(Point point, int number)
        {
            return _game.Grid.Place(point, number, true);
        }

        public void SetState()
        {
            _game.SetState(new DefiniteNumberState(_game));
        }
    }
}