using System;

namespace Sudoku.DependencyInjection.Exceptions
{
    [Serializable]
    public class TypeNotRegisteredException : System.Exception
    {
        public TypeNotRegisteredException(string message): base(message) { }
    }
}