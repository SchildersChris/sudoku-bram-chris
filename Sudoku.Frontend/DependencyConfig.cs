using System;
using Sudoku.DependencyInjection.Container;

namespace Sudoku.Frontend
{
    /// <summary>
    /// Configuration for dependency injection
    /// </summary>
    internal static class DependencyConfig
    {
        #region Dependency Container
        private static readonly Lazy<IContainer>
            LazyContainer = new(() =>
            {
                var container = new DependencyInjectionContainer();
                RegisterTypes(container);

                return container;
            });

        /// <summary>
        /// Gets the configured dependency container
        /// </summary>
        public static IContainer GetContainer() => LazyContainer.Value;

        #endregion

        /// <summary>
        /// Registers the type mappings with the dependency container
        /// </summary>
        /// <param name="container">Container to configure</param>
        private static void RegisterTypes(DependencyInjectionContainer container)
        {
            // Todo: Type registration...
        }
    }
}