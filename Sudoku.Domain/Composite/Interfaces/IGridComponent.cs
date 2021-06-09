using System.Drawing;

namespace Sudoku.Domain.Composite.Interfaces
{
    public interface IGridComponent
    {
        /// <summary>
        /// Checks if a point is contained inside a grid
        /// </summary>
        /// <param name="point">Original point coordinate</param>
        /// <param name="number">Number to check</param>
        /// <param name="gridNumber">Grid number to check</param>
        /// <returns>True if grid contains the number, otherwise false</returns>
        bool Contains(Point point, int number, int gridNumber);
        
        /// <summary>
        /// Checks if a certain number is allowed on a point
        /// </summary>
        /// <param name="point">Point coordinate to check</param>
        /// <param name="number">Number to check</param>
        /// <returns>True if no errors were found, otherwise false</returns>
        bool Check(Point point, int number);

        /// <summary>
        /// This method will place a number on a certain point regardless whether
        /// this placement is valid.
        /// </summary>
        /// <param name="point">Point coordinate to place</param>
        /// <param name="number">Number to place</param>
        /// <param name="isAuxiliary">Whether the placement is a definite number or auxiliary number</param>
        void Place(Point point, int number, bool isAuxiliary);
        
        /// <summary>
        /// This method will layout the grid within a readonly 2d array
        /// </summary>
        /// <param name="cells">Array to layout cells</param>
        void Layout(ICell[,] cells);
    }
}