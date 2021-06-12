namespace Sudoku.Domain.Visitors
{
    public interface ISolverVisitor
    {
        void Visit(GameElement game);
    }
}