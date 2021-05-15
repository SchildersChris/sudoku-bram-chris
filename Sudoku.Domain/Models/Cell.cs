using System.Drawing;
using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Domain.Models
{
    public class Cell : IGrid, ICell
    {
        public Point Point { get; }
        public int? Number { get; private set; }
        public int?[] Temporary { get; }
        public bool Faulty { get; private set; }

        public Cell(Point point, int? number = null)
        {
            Temporary = new int?[9];
            
            Point = point;
            Number = number;
            Faulty = false;
        }

        public int Count()
        {
            return 1;
        }

        public bool Check(int number)
        {
            return Number == number;
        }

        public bool Check(Point point, int number)
        {
            return Point.X == point.X || Point.Y == point.Y && Check(number);
        }

        public bool Place(Point point, int number, bool temporary)
        {
            Faulty = !Check(point, number);
            if (Point != point) 
                return Faulty;
            
            if (temporary)
            {
                Temporary[number - 1] = Temporary[number - 1] == number ? null : number; 
                return true;
            }
            Number = Number == number ? null : number;
            return Faulty;
        }

        public void Layout(ICell[,] cells)
        {
            if (Point.X < cells.GetLength(0) && Point.Y < cells.GetLength(1))
            {
                cells[Point.X, Point.Y] = this;
            }
        }
    }
}