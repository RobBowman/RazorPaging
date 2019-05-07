using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter num 1");
            string num1 = Console.ReadLine();
            Console.WriteLine("Enter num 2");
            string num2 = Console.ReadLine();

            string result = Divide(num1, num2);

            Console.WriteLine($"The result is {result}");

            Console.WriteLine("Enter to quit");
            Console.ReadLine();
        }

        static string Divide(string num1, string num2)
        {

            int result = (Convert.ToInt32(num1) / Convert.ToInt32(num2));

            return result.ToString();

        }
    }
}
