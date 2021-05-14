using System;

namespace Sudoku.DependencyInjection.Container
{
    public interface IContainer
    {
        TResolve Resolve<TResolve>(params object[] args) where TResolve : class;
        object Resolve(Type toResolve, params object[] args);
    }
}