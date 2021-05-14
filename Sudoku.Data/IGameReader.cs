using Sudoku.Domain;

namespace Sudoku.Data
{
    public interface IGameReader
    {
        IGame Read(string path);
    }
}