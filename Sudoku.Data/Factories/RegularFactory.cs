using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Data.Factories
{
    public class RegularFactory : BaseRegularFactory
    {
        protected override int Construct(IEnumerable<string> lines)
        {
            var line = lines.First();
            AddSudoku(line, 0, 0);

            return (int) Math.Sqrt(line.Length);
        }
    }
}