using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    public static class BingoGame
    {
        /// <summary>
        /// Calculate whether we score the bingo or not, based on input.
        /// Assumming that we have 5x5 table as followed:
        /// | 1  | 2  | 3  | 4  | 5  |
        /// | 6  | 7  | 8  | 9  | 10 |
        /// | 11 | 12 | 13 | 14 | 15 |
        /// | 16 | 17 | 18 | 19 | 20 |
        /// | 21 | 22 | 23 | 24 | 25 |
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static bool Bingo(int[] numbers)
        {
            bool[,] table = new bool[5, 5];
            for (int i = 0; i < numbers.Length; i++)
            {
                var n = numbers[i];
                //Validate input
                if (n < 1 || n > 25)
                {
                    Console.WriteLine("All of the numbers must be in range between 1-25.");
                    return false;
                }
                var row = Math.Ceiling(n / 5f) - 1;
                var col = (n - 1) % 5;
                //Console.WriteLine($"{row} : {col}");
            }
            return false;
        }
    }
}
