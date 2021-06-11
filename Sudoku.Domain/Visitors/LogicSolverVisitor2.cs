using System;
using System.Drawing;
using Sudoku.Common.Extensions;

namespace Sudoku.Domain.Visitors
{
    public class LogicSolverVisitor2 : ISolverVisitor
    {
        public void Visit(GameElement game)
        {
            Solve(game);
        }

        private static bool Solve(GameElement game)
        {
            while (true)
            {
                var (p, s) = FindPoint(game);
                if (p == null)
                {
                    return true;
                }

                game.Grid.Place(p.Value, s, false);

                // Console.WriteLine($"Solving: {p.ToString()}, Number: {s}");
            }
        }

        private static (Point?, int) FindPoint(GameElement game)
        {
            for (var y = 0; y < game.Cells.GetHeight(); y++)
            {
                for (var x = 0; x < game.Cells.GetWidth(); x++)
                {
                    // Check empty
                    var c = game.Cells.Get(x, y);
                    if (c is not { Definite: 0 })
                    {
                        continue;
                    }
                    
                    // Empty
                    var p = new Point(x, y);

                    var s = game.Numbers;
                    var n = 0;
                    for (var i = 1; i <= game.Numbers; i++)
                    {
                        if (!game.Validate(p, i))
                        {
                            s--;
                        }
                        else
                        {
                            n = i;
                        }
                    }
                    
                    if (s == 1)
                    {
                        Console.WriteLine($"Solving: {p.ToString()}, Number: {n}");
                        return (p, n);
                    }
                    
                }
            }
            
            return (null, 0);
        }
    }
}