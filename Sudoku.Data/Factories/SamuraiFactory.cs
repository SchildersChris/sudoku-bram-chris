using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Models;

namespace Sudoku.Data.Factories
{
    public class SamuraiFactory : ISudokuFactory
    {
        private readonly GridBuilder _gridBuilder;

        public SamuraiFactory()
        {
            _gridBuilder = new GridBuilder();
        }
        
        public SudokuModel Create(IEnumerable<string> lines)
        {
            var line = lines.First();
            var length = (int) Math.Sqrt(line.Length);
         
            var subGrids = new List<GridBuilder>();
            for (var i = 0; i < length; i++)
            {
                subGrids.Add(_gridBuilder.AddSubGrid());
            }
            
            throw new System.NotImplementedException();
        }
    }
}