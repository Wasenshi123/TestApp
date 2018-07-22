using System;

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
                int row = (int)Math.Ceiling(n / 5f) - 1;
                int col = (n - 1) % 5;
                //Console.WriteLine($"{row} : {col}");
                table[row, col] = true;
            }

            for (int i = 0; i < 5; i++)
            {
                if (CheckRow(table, i))
                {
                    return true;
                }
                if (CheckCol(table, i))
                {
                    return true;
                }
            }
            if (CheckDiagonal(table))
            {
                return true;
            }
            if(CheckDiagonal(table, true))
            {
                return true;
            }

            return false;
        }

        private static bool CheckCol(bool[,] table, int seed)
        {
            var bingo = true;
            var col = seed;
            for (int ii = 0; ii < 5; ii++)
            {
                var row = ii;
                if (!table[row, col])
                {
                    bingo = false;
                    break;
                }
            }
            return bingo;
        }

        private static bool CheckRow(bool[,] table, int seed)
        {
            var bingo = true;
            var row = seed;
            for (int ii = 0; ii < 5; ii++)
            {
                var col = ii;
                if (!table[row, col])
                {
                    bingo = false;
                    break;
                }
            }
            return bingo;
        }

        private static bool CheckDiagonal(bool[,] table, bool inverse = false)
        {
            var bingo = true;
            for (int i = 0; i < 5; i++)
            {
                var col = inverse ? 4 - i : i;
                if (!table[i, col])
                {
                    bingo = false;
                    break;
                }
            }
            return bingo;
        }
    }
}
