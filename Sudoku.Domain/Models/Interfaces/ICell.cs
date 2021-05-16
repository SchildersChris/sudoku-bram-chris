namespace Sudoku.Domain.Models.Interfaces
{
    public interface ICell
    {
        public int GridNumber { get; }
        public int? Definite { get; }
        public int?[] Auxiliary { get; }
        public bool Faulty { get; }
    }
}