using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Sudoku.Common.Extensions;
using Sudoku.Domain.Composite.Interfaces;

namespace Sudoku.Domain.Visitors
{
    public class BoxLogicSolverVisitor : ISolverVisitor
    {
        public void Visit(GameElement game)
        {
            Solve2(game);
        }

        private static bool Solve(GameElement game)
        {
            var (p, s) = FindPoint(game);
            if (p == null)
            {
                return true;
            }
            
            game.Grid.Place(p.Value, s, false);

            Console.WriteLine($"Solving: {p.ToString()}, Number: {s}");
            
            if (Solve(game))
            {
                return true;
            }
            
            game.Grid.Place(p.Value, 0, false);
        
            
            return false;
        }


        private static void Solve2(GameElement game)
        {
            var groups = new Dictionary<int, List<(Point, ICell)>>();

            var cells = game.Cells;
            for (var y = 0; y < cells.GetHeight(); y++)
            {
                for (var x = 0; x < cells.GetWidth(); x++)
                {
                    var c = cells.Get(x, y);
                    if (c == null)
                    {
                        continue;
                    }

                    var n = c.GridNumber;
                    if (!groups.ContainsKey(n))
                    {
                       groups.Add(n, new List<(Point, ICell)>()); 
                    }
                    
                    groups[n].Add((new Point(x, y), c));
                }
            }
            
            var found = false;
            do
            {
                foreach (var group in groups)
                {
                    foreach (var (p, c) in group.Value)
                    {
                        if (c.Definite == 0)
                        {
                            // c.Definite.
                        }
                    }
                }
            } while (!found);





            // var found = false;
            // do
            // {
            //     var cells = game.Cells;
            //     for (var y = 0; y < cells.GetHeight(); y++)
            //     {
            //         for (var x = 0; x < cells.GetWidth(); x++)
            //         {
            //             // Check for empty cell
            //             var c = cells.Get(x, y);
            //             if (game.Errors.Get(x, y) != true && c is not {Definite: 0})
            //             {
            //                 continue;
            //             }
            //             found = true;
            //             
            //             
            //         }
            //     }
            // } while (found);
        }

        
        private static void CheckBox(GameElement game, Point point)
        {
            var cells = game.Cells;
            for (var y = 0; y < cells.GetHeight(); y++)
            {
                for (var x = 0; x < cells.GetWidth(); x++)
                {
                                
                }
                            
            }
        }

        private static void ClearErrors(GameElement game)
        {
            for (var y = 0; y < game.Errors.GetHeight(); y++)
            {
                for (var x = 0; x < game.Errors.GetWidth(); x++)
                {
                    if (game.Errors.Get(x, y) == true)
                    {
                        game.Grid.Place(new Point(x, y), 0, false);
                    }
                }
            }
        }
        
        private static (Point?, int) FindPoint(GameElement game)
        {
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
                        return (p, n);
                    }
                    
                }
            }
            
            return (null, 0);
        }
    }
}