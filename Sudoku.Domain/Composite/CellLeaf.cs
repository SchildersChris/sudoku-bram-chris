﻿using System.Drawing;
using Sudoku.Domain.Composite.Interfaces;

namespace Sudoku.Domain.Composite
{
    public class CellLeaf : IGridComponent, ICell
    {
        private readonly Point _point;
        public int GridNumber { get; }
        public int Definite { get; private set; }
        public int[] Auxiliary { get; }
        public bool Faulty { get; private set; }

        public CellLeaf(Point point, int gridNumber, int totalAuxiliary, int number)
        {
            _point = point;
            GridNumber = gridNumber;
            Definite = number;
            Auxiliary = new int[totalAuxiliary];
            Faulty = false;
        }

        public bool Contains(int number)
        {
            return Definite == number;
        }
        
        public void Place(Point point, int number, bool temporary)
        {
            if (_point != point)
            {
                return;
            }

            if (temporary)
            {
                if (Definite != 0)
                {
                    return;
                }
                Auxiliary[number - 1] = Auxiliary[number - 1] == number ? 0 : number;
                return;
            }

            Definite = Definite == number ? 0 : number;
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