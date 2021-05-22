using System;

namespace Sudoku.Common.Extensions
{
    public static class IntExtensions
    {
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
            for (var i = 1; i <= len; i++)
            {
                for (var j = len; j > 0; j--)
                {
                    if (j * i == origin)
                    {
                        return (j, i);
                    }  
                }
            }

            throw new ArgumentException("Unable to find factors", nameof(origin));
        }
    }
}