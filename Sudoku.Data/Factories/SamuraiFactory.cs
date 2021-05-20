using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Sudoku.Data.Extensions;
using Sudoku.Domain;

namespace Sudoku.Data.Factories
{
    public class SamuraiFactory : BaseRegularFactory
    {
        private readonly GridBuilder _gridBuilder;

        public SamuraiFactory()
        {
            _gridBuilder = new GridBuilder();
        }
        
        public override IGame Create(IEnumerable<string> lines)
        {
            var linesArr = lines as string[] ?? lines.ToArray();
            if (linesArr.Length != 5)
                throw new ArgumentException("Invalid amount of lines for a SamuraiSudoku", nameof(lines));

            CreateSudoku(linesArr[0], 0, 0);
            CreateSudoku(linesArr[1], 12, 0);
            CreateSudoku(linesArr[2], 12, 12);
            CreateSudoku(linesArr[3], 0, 12);
            CreateSudoku(linesArr[4], 12, 12);

            return new Game(21, _gridBuilder.Build());
        }
    }
}