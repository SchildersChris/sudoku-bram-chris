using System.Drawing;
using Sudoku.Domain.Composite.Interfaces;

namespace Sudoku.Domain.Visitors
{
    public class BackTrackingSolverVisitor : ISolverVisitor
    {
        private const int Number = 9;

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

            for (var i = 1; i <= Number; i++)
            {
                var p = empty.Value;
                if (!game.Check(p, i))
                {
                    continue;
                }

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
            for (var y = 0; y < errors.GetLength(0); y++)
            {
                for (var x = 0; x < errors.GetLength(1); x++)
                {
                    if (errors[y, x] == true || cells[y, x] != null && cells[y, x].Definite == 0)
                    {
                        return new Point(x, y);
                    }
                }
            }
            
            return null;
        }
    }
}