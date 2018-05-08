using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ConsoleApp;
using ConsoleApp1;

namespace ConsoleApp.Test1
{
    public class Calculator_tests
    {
        [Fact]
        public void Adding_2_positive_integers()
        {
            // arrange
            var x = 5;
            var y = 10;
            var calc = new ExampleCalculator();

            // act
            var result = calc.Add(x, y);

            // assert
            Assert.Equal(15, result);
        }

        [Fact]
        public void Subtracting_2_positive_integers()
        {
            // arrange
            var x = 5;
            var y = 10;
            var calc = new ExampleCalculator();

            // act
            var result = calc.Subt(x, y);

            // assert
            Assert.Equal(-5, result);
        }

        [Theory, 
            InlineData(5, 10, 0.5),
            InlineData(1.5, 2, 0.75),
            InlineData(5, 0, double.PositiveInfinity),
            InlineData(-5, 0, double.NegativeInfinity),
            InlineData(0, 0, double.NaN)
            ]
        public void Dividing_2_positive_integers_parametrized(double x, double y, double expected_result)
        {
            // arrange         
            var calc = new ExampleCalculator();

            // act
            double  result = calc.Div(x, y);

            // assert
            Assert.Equal(expected_result, result);
        }

        [Fact]
        public void Mulipliing_2_positive_integers()
        {
            // arrange
            var x = 5;
            var y = 10;
            var calc = new ExampleCalculator();

            // act
            var result = calc.Mult(x, y);

            // assert
            Assert.Equal(50, result);
        }
    }
}
