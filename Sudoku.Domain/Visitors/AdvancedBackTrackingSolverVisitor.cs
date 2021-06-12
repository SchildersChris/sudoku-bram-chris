using System.Collections.Generic;
using System.Drawing;
using Sudoku.Common.Extensions;
using Sudoku.Domain.Composite.Interfaces;

namespace Sudoku.Domain.Visitors
{
    public class AdvancedBackTrackingSolverVisitor : ISolverVisitor
    {
        public void Visit(GameElement game)
        {
            ClearErrors(game);
            Solve(game, SplitGroups(game.Cells));
        }
        
        private static bool Solve(GameElement game, Dictionary<int, List<(Point, ICell)>> groups)
        {
            var empty = FindEmpty(game, groups);
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
                if (Solve(game, groups))
                {
                    return true;
                }

                game.Grid.Place(p, 0, false);
            }
            
            return false;
        }
        
        private static Point? FindEmpty(GameElement game, Dictionary<int, List<(Point, ICell)>> groups)
        {
            var cells = game.Cells;
            foreach (var (_, value) in groups)
            {
                for (var i = 1; i <= game.Numbers; i++)
                {   
                    var any = false;
                    // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
                    foreach (var x in value)
                    {
                        if (x.Item2.Definite != i)
                        {
                            continue;
                        }
                        any = true;
                        break;
                    }
                        
                    if (any)
                    {
                        continue;
                    }

                    var solutions = 0;
                    var idx = 0;
                    for (var j = 0; j < value.Count; j++)
                    {
                        var (p, c) = value[j];
                        if (c.Definite != 0 || !game.Validate(p, i))
                        {
                            continue;
                        }

                        solutions++;
                        idx = j;
                        if (solutions > 1)
                        {
                            break;
                        }
                    }

                    if (solutions != 1)
                    {
                        continue;
                    }

                    game.Grid.Place(value[idx].Item1, i, false);
                }
            }
            
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
        
        private static void ClearErrors(GameElement game)
        {
            for (var y = 0; y < game.Cells.GetHeight(); y++)
            {
                for (var x = 0; x < game.Cells.GetWidth(); x++)
                {
                    var c = game.Cells.Get(x, y);
                    if (c is { Error: true })
                    {
                        game.Grid.Place(new Point(x, y), 0, false);
                    }
                }
            }
        }
        
        private static Dictionary<int, List<(Point, ICell)>> SplitGroups(ICell[,] cells)
        {
            var groups = new Dictionary<int, List<(Point, ICell)>>();
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

            return groups;
        }
    }
}