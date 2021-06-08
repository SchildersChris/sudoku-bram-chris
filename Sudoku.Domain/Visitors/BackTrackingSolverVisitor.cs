using System;
using System.Drawing;
using Sudoku.Common.Extensions;
using Sudoku.Domain.Composite.Interfaces;

namespace Sudoku.Domain.Visitors
{
    public class BackTrackingSolverVisitor : ISolverVisitor
    {
        public void Visit(GameElement game)
        {
            Solve(game);
        }

        private static bool Solve(GameElement game)
        {
            var empty = FindEmpty(game.Errors, game.Cells);
            if (empty == null)
            {
                return true;
            }

            for (var i = 1; i <= game.Numbers; i++)
            {
                var p = empty.Value;
                if (!game.Validate(p, i))
                {
                    continue;
                }

                Console.WriteLine($"Solving: {p.ToString()}, Number: {i}");
                
                game.Grid.Place(p, i, false);
                if (Solve(game))
                {
                    return true;
                }
                    
                game.Grid.Place(p, 0, false);
            }
            
            return false;
        }
        
        
        private static Point? FindEmpty(bool?[,] errors, ICell[,] cells)
        {
            for (var y = 0; y < errors.GetHeight(); y++)
            {
                for (var x = 0; x < errors.GetWidth(); x++)
                {
                    var c = cells.Get(x, y);
                    if (errors.Get(x, y) == true || c is { Definite: 0 })
                    {
                        return new Point(x, y);
                    }
                }
            }
            
            return null;
        }
    }
}