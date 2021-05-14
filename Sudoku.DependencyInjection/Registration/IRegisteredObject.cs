using System;

namespace Sudoku.DependencyInjection.Registration
{
    public interface IRegisteredObject
    {
        Type ToResolve { get; }
        Type Concrete { get; }
        LifeTime LifeTime { get; }
        object Instance { get; }
        void CreateInstance(params object[] args);
    }
}