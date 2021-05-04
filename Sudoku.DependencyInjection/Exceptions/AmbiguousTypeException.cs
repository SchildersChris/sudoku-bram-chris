using System;

namespace Sudoku.DependencyInjection.Exceptions
{
    [Serializable]
    public class AmbiguousTypeException : Exception
    {
        public AmbiguousTypeException(string message) : base(message) { }
    }
}