using System;
using System.Linq;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            RunBingo();


            Console.ReadLine();
        }

        static void RunBingo()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Input the set of numbers:\n");
                    var input = Console.ReadLine();

                    var numbers = input.Split(new[] { " ", ","}, StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();

                    var result = BingoGame.Bingo(numbers);

                    Console.WriteLine($"Input: [{numbers.Aggregate("", (a, b) => a + ", " + b).Remove(0, 2)}] Result = {(result ? "Bingo" : "Not Bingo")}");
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Wrong input format. HINT: Only integer numbers are allowed.");
            }
        }

        static void RunCalculator()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Input the formula:\n");
                    var input = Console.ReadLine();

                    var result = FormulaCalculator.Calculate(input);
                    Console.WriteLine($"result : {result}");
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Wrong input format. HINT: Only support [ +, -, *, / ] and no fraction numbers allowed.");
            }
        }
    }
}
