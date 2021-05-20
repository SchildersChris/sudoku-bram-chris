using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Sudoku.Data.Extensions;
using Sudoku.Domain;

namespace Sudoku.Data.Factories
{
    public class RegularFactory : BaseRegularFactory
    {
        private readonly GridBuilder _gridBuilder;

        public RegularFactory()
        {
            _gridBuilder = new GridBuilder();
        }

        public override IGame Create(IEnumerable<string> lines)
        {
            var line = lines.First();
            var length = (int) Math.Sqrt(line.Length);
            
            CreateSudoku(line, 0, 0);

            return new Game(length, _gridBuilder.Build());
        }
    }
}