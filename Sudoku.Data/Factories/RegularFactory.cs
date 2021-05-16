using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Sudoku.Domain.Models;

namespace Sudoku.Data.Factories
{
    public class RegularFactory : ISudokuFactory
    {
        private readonly GridBuilder _gridBuilder;
        
        public RegularFactory()
        {
            _gridBuilder = new GridBuilder();
        }
        
        public SudokuModel Create(IEnumerable<string> lines)
        {
            var line = lines.First();
            var length = (int) Math.Sqrt(line.Length);
            var grid = (int) Math.Sqrt(length);
            
            // Todo: Fix parser
            for (var i = 0; i < grid; i++)
            {
                for (var j = 0; j < grid; j++)
                {
                    var subGrid = _gridBuilder.AddSubGrid();
                    for (var y = j * grid; y < (j+1) * grid; y++)
                    {   
                        for (var x = i * grid; x < (i+1) * grid; x++)
                        {
                            subGrid.AddCell(new Point(x, y), int.Parse(line[y * length + x].ToString()));
                        }
                    }
                    
                }
            }
            
            return new SudokuModel(length, _gridBuilder.Build());
        }
    }
}