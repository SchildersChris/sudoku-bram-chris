using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Data.Factories
{
    public class SamuraiFactory : BaseRegularFactory
    {
        protected override int Construct(IEnumerable<string> lines)
        {
            var linesArr = lines as string[] ?? lines.ToArray();
            if (linesArr.Length != 5)
            {
                throw new ArgumentException("Invalid amount of lines for a SamuraiSudoku", nameof(lines));
            }

            AddSudoku(linesArr[0], 0, 0);
            AddSudoku(linesArr[1], 12, 0);
            AddSudoku(linesArr[2], 6, 6);
            AddSudoku(linesArr[3], 0, 12);
            AddSudoku(linesArr[4], 12, 12);

            return 21;
        }
    }
}