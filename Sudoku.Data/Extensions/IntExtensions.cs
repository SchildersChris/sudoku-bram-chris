using System;
using System.Collections.Generic;

namespace Sudoku.Data.Extensions
{
    public static class IntExtensions
    {
        public static (int, int) Factorize(this int origin)
        {
            var sqrt = Math.Sqrt(origin);
            var a = (int) sqrt;
            var b = (int) sqrt;

            if (sqrt % 1 == 0)
                return (a, b);
            
            var pairs = new List<(int, int)>();
            var len = (int)Math.Ceiling(sqrt);
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