namespace Sudoku.Domain.Solvers
{
    public class BackTrackingSolver : ISolver
    {
        public void Visit(IGame game)
        {
            var cells = game.Cells;
            foreach (var cell in cells)
            {
                // Todo: Add backtracking ding
            }
        }
    }
}