namespace Sudoku.Domain.Solvers
{
    public interface ISolverVisitor
    {
        void Visit(GameElement game);
    }
}