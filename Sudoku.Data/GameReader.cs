using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sudoku.Data.Factories;
using Sudoku.Domain;

namespace Sudoku.Data
{
    public class GameReader : IGameReader
    {
        private static readonly Dictionary<string, Type> Strategies = new()
        {
            {"jigsaw", typeof(JigsawFactory)},
            {"samurai", typeof(SamuraiFactory)},
            {"4x4", typeof(RegularFactory)},
            {"6x6", typeof(RegularFactory)},
            {"9x9", typeof(RegularFactory)},
        };

        public IGameElement Read(string path)
        {
            var extension = new string(Path.GetExtension(path).Skip(1).ToArray());
            if (!Strategies.ContainsKey(extension))
            {
                throw new ArgumentException($"Cannot read extension: '{extension}'");
            }

            var type = Strategies[extension];
            if (Activator.CreateInstance(type) is not ISudokuFactory factory)
            {
                throw new InvalidCastException($"Something went wrong while creating sudoku generator");
            }

            try
            {
                return factory.Create(File.ReadLines(path));
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to create sudoku from file", ex);
            }
        }
    }
}