using System.Drawing;

namespace Sudoku.Domain.Visitors
{
    public class BackTrackingSolverVisitor : ISolverVisitor
    {
        public void Visit(GameElement game)
        {
            var cells = game.Cells;
            var grid = game.Grid;
            
            for (var y = 0; y < cells.GetLength(0); y++)
            {
                for (var x = 0; x < cells.GetLength(1); x++)
                {
                    for (var i = 1; i <= 9; i++)
                    {
                        var p = new Point(x, y);
                        if (grid.Check(p, i, false))
                        {
                            grid.Place(p, i, false);
                        }
                    }
                }
            }
        }
    }
}