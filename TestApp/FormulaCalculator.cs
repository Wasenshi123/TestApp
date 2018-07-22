using System.Collections.Generic;
using System.Linq;

namespace TestApp
{
    public static class FormulaCalculator
    {
        /// <summary>
        /// Calucalate formula string
        /// </summary>
        /// <param name="formula"></param>
        /// <returns></returns>
        public static double Calculate(string formula)
        {
            Stack<double> numbers = new Stack<double>();
            Stack<char> ops = new Stack<char>();

            Stack<double> numberList = new Stack<double>();
            Stack<char> opList = new Stack<char>();

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
                else if (char.IsWhiteSpace(ch))
                {
                    continue;
                }
                else if (ch == ')')
                {
                    numberList.Push(numbers.Count > 0 ? numbers.Pop() : 0);
                    while (ops.Count > 0)
                    {
                        var op = ops.Pop();
                        if (op == '(')
                        {
                            break;
                        }
                        opList.Push(op);
                        numberList.Push(numbers.Count > 0 ? numbers.Pop() : 0);
                    }
                    double subResult = CalculateGroupWithPriority(numberList, opList);
                    numbers.Push(subResult);
                }
                else
                {
                    ops.Push(formula[i]);
                }
            }

            if (numbers.Count > 0)
            {
                numberList.Push(numbers.Count > 0 ? numbers.Pop() : 0);
                while (ops.Count > 0)
                {
                    var op = ops.Pop();
                    opList.Push(op);
                    numberList.Push(numbers.Count > 0 ? numbers.Pop() : 0);
                }
            }
            numberList.Reverse();
            opList.Reverse();
            result = CalculateGroupWithPriority(numberList, opList);

            return result;
        }

        private static double CalculateGroupWithPriority(Stack<double> numberList, Stack<char> opList)
        {
            Stack<double> nums = new Stack<double>();
            Stack<char> ops = new Stack<char>();
            //use stack to calculate first the group of + and -
            while (opList.Count > 0)
            {
                if (opList.Peek() == '+' || opList.Peek() == '-')
                {
                    Stack<double> subGroupNumb = new Stack<double>();
                    Stack<char> subOp = new Stack<char>();

                    subGroupNumb.Push(numberList.Pop());
                    while (opList.Count > 0 && (opList.Peek() == '+' || opList.Peek() == '-'))
                    {
                        subGroupNumb.Push(numberList.Pop());
                        subOp.Push(opList.Pop());
                    }
                    nums.Push(CaluculateSet(new Stack<double>(subGroupNumb), new Stack<char>(subOp)));
                }
                else
                {
                    nums.Push(numberList.Pop());
                    ops.Push(opList.Pop());
                }
            }
            if (numberList.Count > 0)
            {
                nums.Push(numberList.Pop());
            }

            double result = CaluculateSet(new Stack<double>(nums), new Stack<char>(ops));

            return result;
        }

        private static double CaluculateSet(Stack<double> numberList, Stack<char> opList)
        {
            double result = numberList.Pop();
            while (opList.Count > 0)
            {
                var op = opList.Pop();
                var second = numberList.Pop();
                result = CalculateUnit(result, second, op);
            }

            return result;
        }

        private static double CalculateUnit(double first, double second, char op)
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
