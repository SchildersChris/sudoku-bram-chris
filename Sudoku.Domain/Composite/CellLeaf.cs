using System.Drawing;
using Sudoku.Domain.Composite.Interfaces;

namespace Sudoku.Domain.Composite
{
    public class CellLeaf : IGridComponent, ICell
    {
        private readonly Point _point;
        public int GridNumber { get; }
        public int Definite { get; private set; }
        public int[] Auxiliary { get; }

        public CellLeaf(Point point, int gridNumber, int totalAuxiliary, int number)
        {
            _point = point;
            GridNumber = gridNumber;
            Definite = number;
            Auxiliary = new int[totalAuxiliary];
        }

        public bool CheckInverted(Point point, int number)
        {
            return _point != point && Definite == number;
        }

        public bool Place(Point point, int number, bool isAuxiliary)
        {
            if (_point != point)
            {
                return !((_point.X == point.X || _point.Y == point.Y) && Definite == number);
            }

            if (isAuxiliary)
            {
                if (Definite == 0)
                {
                    Auxiliary[number - 1] = Auxiliary[number - 1] == number ? 0 : number;
                }
            }
            else
            {
                Definite = Definite == number ? 0 : number;
            }
            return true;
        }

        public void Layout(ICell[,] cells)
        {
            if (_point.X >= cells.GetLength(1) || _point.Y >= cells.GetLength(0)) 
                return;
            
            if (cells[_point.Y, _point.X] == null || cells[_point.Y, _point.X].Definite != 0)
            {
                cells[_point.Y, _point.X] = this;
            }
        }
    }
}
