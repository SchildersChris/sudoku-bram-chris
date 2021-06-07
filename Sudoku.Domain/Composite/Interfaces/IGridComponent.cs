using System.Drawing;

namespace Sudoku.Domain.Composite.Interfaces
{
    public interface IGridComponent
    {
        /// <summary>
        /// Checks if a certain number is allowed on a point
        /// </summary>
        /// <param name="point">Point coordinate to check</param>
        /// <param name="number">Number to check</param>
        /// <param name="match">
        /// If match is false: then this method will check whether the number's match and the point's do not match.
        /// If this is true then the result will be true, otherwise the function will return false.
        ///
        /// If match is true: then this method will check whether the number's match and the point's align on any axis
        /// If this is true then the result will be true, otherwise the function will return true.
        /// </param>
        /// <returns>True if check is successful, otherwise false</returns>
        bool Check(Point point, int number, bool match);
        
        /// <summary>
        /// This method will place a number on a certain point regardless whether
        /// this placement is valid.
        /// </summary>
        /// <param name="point">Point coordinate to place</param>
        /// <param name="number">Number to place</param>
        /// <param name="isAuxiliary">Whether the placement is a definite number or auxiliary number</param>
        /// <returns>True if the placement was valid, otherwise false</returns>
        bool Place(Point point, int number, bool isAuxiliary);
        
        /// <summary>
        /// This method will layout the grid within a readonly 2d array
        /// </summary>
        /// <param name="cells">Array to layout cells</param>
        void Layout(ICell[,] cells);
    }
}