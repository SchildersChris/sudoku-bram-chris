using System.Collections.Generic;
using System.Drawing;
using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Domain.Models
{
    public class Cell : IGrid, ICell
    {
        public Point Point { get; }
        public int? Number { get; private set; }
        public bool Temporary { get; private set; }
        public bool Faulty { get; private set; }

        public Cell(Point point, int? number = null)
        {
            Point = point;
            Number = number;
            Temporary = false;
            Faulty = false;
        }

        public IEnumerable<ICell[]> GetCells()
        {
            yield return new ICell[] { this };
        }

        public bool Check(Point point, int number)
        {
            return Point.X == point.X || Point.Y == point.Y && number == Number;
        }

        public bool Place(Point point, int number, bool temporary)
        {
            Faulty = !Check(point, number);
            Temporary = temporary;
            Number = number;
            
            return Faulty;
        }
    }
}