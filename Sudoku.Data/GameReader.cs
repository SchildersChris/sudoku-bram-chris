using System;
using System.Collections.Generic;
using System.IO;
using Sudoku.Data.Factories;
using Sudoku.Domain;

namespace Sudoku.Data
{
    public class GameReader : IGameReader
    {
        private static readonly Dictionary<string, Type> Strategies = new()
        {
            {".jigsaw", typeof(JigsawFactory)},
            {".samurai", typeof(SamuraiFactory)},
            {".4x4", typeof(RegularFactory)},
            {".6x6", typeof(RegularFactory)},
            {".9x9", typeof(RegularFactory)},
        };

        public IGame Read(string path)
        {
            var extension = Path.GetExtension(path);
            if (extension == null)
            {
                throw new ArgumentException($"The the following path: '{path}' is not valid", nameof(path));
            }

            if (!Strategies.ContainsKey(extension))
            {
                throw new ArgumentException($"There is no strategy registered for: '{extension}'", nameof(extension));
            }

            var type = Strategies[extension];
            if (Activator.CreateInstance(type) is not ISudokuFactory factory)
            {
                throw new InvalidCastException($"Failed to create instance of {type.Name}");
            }

            var (len, grid) = factory.Create(File.ReadLines(path));
            return new Game(len, grid);
        }
    }
}