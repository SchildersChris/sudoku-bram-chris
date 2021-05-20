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
            foreach (var line in lines)
            {
                var length = (int) Math.Sqrt(line.Length);
                var gridLen = Math.Sqrt(length);
                
                var width = (int) gridLen;
                var height = (int) gridLen;

                var subGrids = new List<GridBuilder>();
                for (var i = 0; i < length; i++)
                {
                    subGrids.Add(_gridBuilder.AddSubGrid());
                }
            }
            
            throw new System.NotImplementedException();
        }
    }
}