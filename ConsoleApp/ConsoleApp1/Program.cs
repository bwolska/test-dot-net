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
            double result;

            Console.WriteLine("Witamy");

            Console.Write("Op1: ");
            var x = double.Parse(Console.ReadLine());

            Console.Write("Op2: ");
            var y = double.Parse(Console.ReadLine());

            Console.Write("Wybierz operacje: ");
            var op = char.Parse(Console.ReadLine());

            var calc = new ExampleCalculator();
          
            switch (op)
            {
                case '+' :
                    result = calc.Add(x, y);
                    break;

                case '-':
                    result = calc.Subt(x, y);
                    break;

                case '*':
                    result = calc.Mult(x, y);
                    break;

                case '/':
                    result = calc.Div(x, y);
                    break;

                default:
                    result = 0;
                    break;

            }

            Console.Write("Wynik: ");
            Console.WriteLine(result);

            Console.ReadKey();
        }
    }

    public class ExampleCalculator
    {
        public ExampleCalculator()
        {
        }

        public double Add(double x, double y)
        {
            return x + y;
        }

        public double Subt(double x, double y)
        {
            return x - y;
        }

        public double Mult(double x, double y)
        {
            return x * y;
        }

        public double Div(double x, double y)
        {
            return (double) x / y;
        }

    }
}
