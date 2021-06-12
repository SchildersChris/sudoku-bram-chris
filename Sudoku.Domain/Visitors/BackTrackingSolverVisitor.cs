using System.Drawing;
using Sudoku.Common.Extensions;
using Sudoku.Domain.Composite.Interfaces;

namespace Sudoku.Domain.Visitors
{
    public class BackTrackingSolverVisitor : ISolverVisitor
    {
        public void Visit(GameElement game)
        {
            for (var y = 0; y < game.Cells.GetHeight(); y++)
            {
                for (var x = 0; x < game.Cells.GetWidth(); x++)
                {
                    var c = game.Cells.Get(x, y);
                    if (c is { Error: { } })
                    {
                        game.Grid.Place(new Point(x, y), 0, false);
                    }
                }
            }

            Solve(game);
        }

        private static bool Solve(GameElement game)
        {
            var empty = FindEmpty(game.Cells);
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
                
                // Console.WriteLine($"Solving: {p.ToString()}, Number: {i}");
                
                game.Grid.Place(p, i, false);
                if (Solve(game))
                {
                    return true;
                }

                game.Grid.Place(p, 0, false);
            }
            
            return false;
        }

        private static Point? FindEmpty(ICell[,] cells)
        {
            for (var y = 0; y < cells.GetHeight(); y++)
            {
                for (var x = 0; x < cells.GetWidth(); x++)
                {
                    var c = cells.Get(x, y);
                    if (c != null && (c.Error == true || c.Definite == 0))
                    {
                        return new Point(x, y);
                    }
                }
            }
            
            return null;
        }
    }
}