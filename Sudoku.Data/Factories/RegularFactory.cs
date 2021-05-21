using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Composite.Interfaces;

namespace Sudoku.Data.Factories
{
    public class RegularFactory : BaseRegularFactory
    {
        public override (int, IGridComponent) Create(IEnumerable<string> lines)
        {
            var line = lines.First();
            AddSudoku(line, 0, 0);
            return ((int) Math.Sqrt(line.Length), Build());
        }
    }
}