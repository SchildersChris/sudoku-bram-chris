using System.Drawing;

namespace Sudoku.Domain.Models.Interfaces
{
    public interface ICell
    {
        public Point Point { get; }
        public int? Number { get; }
        public bool Temporary { get; }
        public bool Faulty { get; }
    }
}