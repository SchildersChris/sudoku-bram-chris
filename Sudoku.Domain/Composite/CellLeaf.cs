using System.Drawing;
using Sudoku.Common.Extensions;
using Sudoku.Domain.Composite.Interfaces;

namespace Sudoku.Domain.Composite
{
    public class CellLeaf : IGridComponent, ICell
    {
        private readonly Point _point;
        public int GridNumber { get; }
        public int Definite { get; private set; }
        public int[] Auxiliary { get; }
        public bool? Error { get; private set; }

        public CellLeaf(Point point, int gridNumber, int totalAuxiliary, int number)
        {
            _point = point;
            GridNumber = gridNumber;
            Definite = number;
            Auxiliary = new int[totalAuxiliary];
        }

        public bool Contains(Point point, int number, int gridNumber)
        {
            return !(_point != point && Definite == number && GridNumber == gridNumber);
        }

        public bool Check(Point point, int number)
        {
            if (_point == point)
            {
                return true;
            }

            return !((_point.X == point.X || _point.Y == point.Y) && Definite == number);
        }

        public void Place(Point point, int number, bool isAuxiliary)
        {
            if (_point != point)
            {
                return;
            }

            if (isAuxiliary)
            {
                if (Definite != 0)
                {
                    return;
                }

                Auxiliary[number - 1] = Auxiliary[number - 1] == number ? 0 : number;
            }
            else
            {
                Definite = Definite == number ? 0 : number;
            }
        }

        public void SetError(Point point, bool? value)
        {
            if (_point != point)
            {
                return;
            } 
            
            if (Definite == 0)
            {
                Error = null;
                return;
            }
            
            Error = value;
        }

        public void Layout(ICell[,] cells)
        {
            if (!cells.Contains(_point))
            {
                return;
            }

            var c = cells.Get(_point);
            if (c == null || c.Definite == 0 && Definite != 0)
            {
                cells.Set(_point, this);
            }
        }
    }
}