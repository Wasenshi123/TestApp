using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Bingo(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

            Console.ReadLine();
        }

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
        static bool Bingo(int[] numbers)
        {
            bool[,] table = new bool[5,5];
            for (int i = 0; i < numbers.Length; i++)
            {
                var n = numbers[i];
                //Validate input
                if(n < 1 || n > 25)
                {
                    Console.WriteLine("The numbers must be in range between 1-25.");
                    return false;
                }
                var row = Math.Ceiling(n / 5f) - 1;
                var col = (n - 1) % 5;
                //Console.WriteLine($"{row} : {col}");
            }
            return false;
        }

        static double Calculate(string formula)
        {
            Stack<int> numbers = new Stack<int>();
            Stack<char> ops = new Stack<char>();

            float result = 0;
            for (int i = 0; i < formula.Length; i++)
            {
                char ch = formula[i];
                int n;
                if (Int32.TryParse(ch.ToString(), out n))
                {
                    numbers.Push(n);
                }
                else if(char.IsWhiteSpace(ch))
                {
                    continue;
                }
                else if (ch == ')')
                {
                    List<int> numberList = new List<int>();
                    List<char> opList = new List<char>();
                    float subResult = 0;
                    numberList.Add(numbers.Count > 0 ? numbers.Pop() : 0);
                    while (ops.Count > 0)
                    {
                        var op = ops.Pop();
                        if (op == '(')
                        {
                            break;
                        }
                        opList.Add(op);
                        numberList.Add(numbers.Count > 0 ? numbers.Pop() : 0);
                    }
                    numberList.Reverse();
                    opList.Reverse();
                    subResult = CalculateGroup(numberList, opList);
                    result += subResult;
                }
                else
                {
                    ops.Push(formula[i]);
                }
            }

            return double.MinValue;
        }

        static float CalculateGroup(List<int> numberList, List<char> opList)
        {
            float result = numberList[0];
            numberList.RemoveAt(0);
            while (opList.Count > 0)
            {
                var op = opList[0];
                opList.RemoveAt(0);
                var second = numberList[0];
                numberList.RemoveAt(0);
                result = CalculateUnit(result, second, op);
            }

            return 0;
        }

        static float CalculateUnit(float first, int second, char op)
        {
            switch (op)
            {
                case '+':
                    return first + second;
                case '-':
                    return first - second;
                case '*':
                    return first * second;
                case '/':
                    return first / second;
                default:
                    return 0;
            }
        }
    }
}
