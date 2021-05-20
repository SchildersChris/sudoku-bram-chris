namespace Sudoku.Domain.Solvers
{
    public interface ISolver
    {
        void Visit(IGame game);
    }
}