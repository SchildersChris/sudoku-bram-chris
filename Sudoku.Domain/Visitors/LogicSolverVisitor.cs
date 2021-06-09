using System;
using System.Collections.Generic;
using System.Drawing;
using Sudoku.Common.Extensions;

namespace Sudoku.Domain.Visitors
{
    public class LogicSolverVisitor : ISolverVisitor
    {
        public void Visit(GameElement game)
        {
            Solve(game);
        }

        private static bool Solve(GameElement game)
        {
            var (p, solutions) = FindPoint(game);
            if (p == null)
            {
                return true;
            }
            
            foreach (var s in solutions)
            {
                game.Grid.Place(p.Value, s, false);

                // Console.WriteLine($"Solving: {p.ToString()}, Number: {s}");

                if (Solve(game))
                {
                    return true;
                }
            }
            
            return false;
        }

        private static (Point?, List<int>) FindPoint(GameElement game)
        {
            Point? point = null;
            List<int> solutions = null;

            for (var y = 0; y < game.Errors.GetHeight(); y++)
            {
                for (var x = 0; x < game.Errors.GetWidth(); x++)
                {
                    // Check empty
                    var c = game.Cells.Get(x, y);
                    if (game.Errors.Get(x, y) != true && c is not { Definite: 0 })
                    {
                        continue;
                    }
                    
                    // Empty
                    var p = new Point(x, y);
                    var s = new List<int>();
                    
                    for (var i = 1; i <= game.Numbers; i++)
                    {
                        if (game.Validate(p, i))
                        {
                            s.Add(i);
                        }   
                    }

                    // Determine min solutions
                    if (solutions == null)
                    {
                        solutions = s;
                        point = p;
                    } 
                    else if (solutions.Count > s.Count)
                    {
                        solutions = s;
                        point = p;
                    }
                }
            }
            
            return (point, solutions);
        }
    }
}