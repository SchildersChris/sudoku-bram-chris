using Sudoku.Domain;

namespace Sudoku.Data
{
    public interface IGameReader
    {
        IGameElement Read(string path);
    }
}   