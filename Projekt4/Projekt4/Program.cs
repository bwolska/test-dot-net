using System;

namespace Zadanie4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("y = a * (x * x) + b  * y + c");
            Console.Write("podaj wartośc a = ");
            var a = Convert.ToDouble(Console.ReadLine());

            Console.Write("podaj wartośc b = ");
            var b = Convert.ToDouble(Console.ReadLine());

            Console.Write("podaj wartośc c = ");
            var c = Convert.ToDouble(Console.ReadLine());

            Calculate(a, b, c);

            Console.WriteLine("Brawo! Czwarte zadanie zrobione :)");
            Console.WriteLine("Naciśnij jakikolwiek klawisz aby zakończyć...");
            Console.ReadKey();
        }

        private static void Calculate(double a, double b, double c)
        {
            // napisz w tym miejscu obliczanie rozwiązań (miejsc zerowych)
            // funkcji kwadratowej i wypisanie ich na ekran
            //
            // jeśli nie pamiętasz jak to się liczy to tutaj jest ściąga
            // http://www.matematykam.pl/miejsca_zerowe_funkcji_kwadratowej.html
            //
            // pamiętaj, że trzeba obsłużyć trzy przypadki:
            // - brak miejsc zerowych: wtedy wyświetl taki komunikat
            // - jedno miejsce zerowe: wtedy wyświetl komunikat x = ...
            // - dwa miejsca zerowe: wtedy wyświetl komunikat x1 = ... x2 = ...
            //
            // postaraj się napisac to samodzielnie a nie googlując implementacji
            //
            // powodzenia :)
        }
    }
}