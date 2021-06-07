﻿using System.Drawing;
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

        public CellLeaf(Point point, int gridNumber, int totalAuxiliary, int number)
        {
            _point = point;
            GridNumber = gridNumber;
            Definite = number;
            Auxiliary = new int[totalAuxiliary];
        }

        public bool Check(Point point, int number, bool match)
        {
            if (!match)
            {
                return _point != point && Definite == number;
            }
         
            return (_point.X == point.X || _point.Y == point.Y) && Definite == number;
        }
        
        public bool Place(Point point, int number, bool isAuxiliary)
        {
            if (_point != point)
            {
                return isAuxiliary || !Check(point, number, true);
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
            if (!cells.Contains(_point))
            {
                return;
            }
            
            if (cells[_point.Y, _point.X] == null || cells[_point.Y, _point.X].Definite != 0)
            {
                cells[_point.Y, _point.X] = this;
            }
        }
    }
}
