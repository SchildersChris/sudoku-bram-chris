using System;

namespace Sudoku.DependencyInjection.Exceptions
{
    [Serializable]
    public class AmbiguousTypeException : System.Exception
    {
        public AmbiguousTypeException(string message) : base(message) { }
    }
}