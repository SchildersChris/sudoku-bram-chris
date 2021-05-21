namespace Sudoku.Domain.Solvers
{
    public class BackTrackingSolverVisitor : ISolverVisitor
    {
        public void Visit(GameElement game)
        {
            var cells = game.Cells;
            foreach (var cell in cells)
            {
                // Todo: Add backtracking ding
            }
        }
    }
}