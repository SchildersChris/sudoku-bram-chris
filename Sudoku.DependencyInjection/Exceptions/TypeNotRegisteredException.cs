using System;

namespace Sudoku.DependencyInjection.Exceptions
{
    [Serializable]
    public class TypeNotRegisteredException : Exception
    {
        public TypeNotRegisteredException(string message): base(message) { }
    }
}