namespace Sudoku.Domain.Composite.Interfaces
{
    public interface ICell
    {
        public int GridNumber { get; }
        public int Definite { get; }
        public int[] Auxiliary { get; }
        public bool? Error { get; }
    }
}