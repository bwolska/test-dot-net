using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Witamy");

            Console.Write("Op1: ");
            var x = int.Parse(Console.ReadLine());

            Console.Write("Op2: ");
            var y = int.Parse(Console.ReadLine());

            Console.WriteLine("Wybierz operacje: ");
            var op = char.Parse(Console.ReadLine());

            var calc = new ExampleCalculator();
          
           

            var result = calc.Add(x, y);

            Console.WriteLine(result);

            Console.ReadKey();
        }
    }

    public class ExampleCalculator
    {
        public ExampleCalculator()
        {
        }

        public int Add(int x, int y)
        {
            return x + y;
        }

        public int Subt(int x, int y)
        {
            return x - y;
        }

        public int Mult(int x, int y)
        {
            return x * y;
        }

        public double Div(double x, double y)
        {
            return (double) x / y;
        }

    }
}
