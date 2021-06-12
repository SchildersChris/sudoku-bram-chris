using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Data.Factories
{
    public class RegularFactory : BaseRegularFactory
    {
        protected override (int numbers, int length) Construct(IEnumerable<string> lines)
        {
            var line = lines.First();
            AddSudoku(line, 0, 0);

            var len = (int) Math.Sqrt(line.Length);
            return (len, len);
        }
    }
}