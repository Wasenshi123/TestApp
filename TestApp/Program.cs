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

            while (true)
            {
                Console.WriteLine("Input:\n");
                var input = Console.ReadLine();
                var result = Calculate(input);
                Console.WriteLine($"result : {result}");
            }
            //var result = Calculate("5+6");
            //Console.WriteLine($"result : {result}");

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

        /// <summary>
        /// Calucalate formula string
        /// </summary>
        /// <param name="formula"></param>
        /// <returns></returns>
        static double Calculate(string formula)
        {
            Stack<double> numbers = new Stack<double>();
            Stack<char> ops = new Stack<char>();

            List<double> numberList = new List<double>();
            List<char> opList = new List<char>();

            List<int> composeNum = new List<int>();

            double result = 0;
            for (int i = 0; i < formula.Length; i++)
            {
                char ch = formula[i];
                if (char.IsDigit(ch))
                {
                    string temp = ch.ToString();
                    for (; i + 1 < formula.Length && char.IsDigit(formula[i + 1]); i++)
                    {
                        temp += formula[i + 1];
                    }
                    int n = int.Parse(temp);
                    numbers.Push(n);
                }
                else if(char.IsWhiteSpace(ch))
                {
                    continue;
                }
                else if (ch == ')')
                {
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
                    double subResult = CalculateGroup(numberList, opList);
                    numbers.Push(subResult);
                }
                else
                {
                    ops.Push(formula[i]);
                }
            }

            if(numbers.Count > 0)
            {
                numberList.Add(numbers.Count > 0 ? numbers.Pop() : 0);
                while (ops.Count > 0)
                {
                    var op = ops.Pop();
                    opList.Add(op);
                    numberList.Add(numbers.Count > 0 ? numbers.Pop() : 0);
                }
            }
            numberList.Reverse();
            opList.Reverse();
            result = CalculateGroup(numberList, opList);

            return result;
        }

        static double CalculateGroup(List<double> numberList, List<char> opList)
        {
            Stack<double> nums = new Stack<double>(numberList);
            Stack<char> ops = new Stack<char>(opList);
            //use stack to cal first the group of + and -
            for (int i = 0; i < opList.Count; i++)
            {
                if (opList[i] == '+' || opList[i] == '-')
                {
                    List<double> subGroupNumb = new List<double>();
                    var n = i;
                    subGroupNumb.Add(numberList[n]);
                    while (opList[i] == '+' || opList[i] == '-')
                    {

                    }
                }
            }

            double result = numberList[0];
            numberList.RemoveAt(0);
            while (opList.Count > 0)
            {
                var op = opList[0];
                opList.RemoveAt(0);
                var second = numberList[0];
                numberList.RemoveAt(0);
                result = CalculateUnit(result, second, op);
            }

            return result;
        }

        static double CalculateUnit(double first, double second, char op)
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
