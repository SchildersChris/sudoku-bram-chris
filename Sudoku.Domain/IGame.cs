﻿using System.Collections.Generic;
using System.Drawing;
using Sudoku.Domain.Enums;
using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Domain
{
    public interface IGame
    {
        EditorState State { get; set; }
        IEnumerable<IReadonlyGrid> Grids { get; }
        void Solve();
        bool Place(Point point, int number);
    }
}