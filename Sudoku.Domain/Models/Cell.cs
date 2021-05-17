using System.Drawing;
using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Domain.Models
{
    public class Cell : IGrid, ICell
    {
        private readonly Point _point;
        public int GridNumber { get; }
        public int? Definite { get; private set; }
        public int?[] Auxiliary { get; }
        public bool Faulty { get; private set; }

        public Cell(Point point, int gridNumber, int totalAuxiliary, int? number = null)
        {
            _point = point;
            GridNumber = gridNumber;
            Definite = number;
            Auxiliary = new int?[totalAuxiliary];
            Faulty = false;
        }

        public int Count()
        {
            return 1;
        }

        public bool Check(int number)
        {
            return Definite == number;
        }

        public bool Check(Point point, int number)
        {
            return _point.X == point.X || _point.Y == point.Y && Check(number);
        }

        public bool Place(Point point, int number, bool temporary)
        {
            Faulty = !Check(point, number);
            if (_point != point)
                return Faulty;

            if (temporary)
            {
                Auxiliary[number - 1] = Auxiliary[number - 1] == number ? null : number;
                return true;
            }

            Definite = Definite == number ? null : number;
            return Faulty;
        }

        public void Layout(ICell[,] cells)
        {
            if (_point.X < cells.GetLength(1) && _point.Y < cells.GetLength(0))
            {
                cells[_point.Y, _point.X] = this;
            }
        }
    }
}