using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain;

namespace Sudoku.Data.Factories
{
    public class RegularFactory : BaseRegularFactory
    {
        public override IGame Create(IEnumerable<string> lines)
        {
            var line = lines.First();
            var length = (int) Math.Sqrt(line.Length);
            
            AddSudoku(line, 0, 0);

            return new Game(length, Build());
        }
    }
}