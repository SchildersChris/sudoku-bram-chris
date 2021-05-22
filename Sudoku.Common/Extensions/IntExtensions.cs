using System;
using System.Collections.Generic;

namespace Sudoku.Common.Extensions
{
    public static class IntExtensions
    {
        /// <summary>
        /// This method will factorize a given natural number into its optimal factors.
        /// Given a number x this algorithm will find the optimal factors a and b, so a * b = x
        /// </summary>
        /// <param name="origin">Natural number to be factorized</param>
        /// <returns>Optimal factors in two natural numbers</returns>
        /// <exception cref="ArgumentException">The provided number is not the product of two natural numbers</exception>
        public static (int, int) Factorize(this int origin)
        {
            var sqrt = Math.Sqrt(origin);
            var a = (int) sqrt;
            var b = (int) sqrt;

            if (sqrt % 1 == 0)
            {
                return (a, b);
            }
            
            var len = origin / 2;
            var pairs = new List<(int, int)>();
            for (var i = 1; i <= len; i++)
            {
                for (var j = len; j > 0; j--)
                {
                    if (j * i == origin)
                    {
                        pairs.Add((j, i));
                    }  
                }
            }

            if (pairs.Count == 0)
            {
                throw new ArgumentException("The provided number is not the product of two natural numbers", nameof(origin));
            }

            var min = int.MaxValue;
            foreach (var (j, i) in pairs)
            {
                var sum = j + i;
                if (sum >= min) 
                    continue;

                min = sum;
                a = j;
                b = i;
            }
            
            return (a, b);
        }
    }
}