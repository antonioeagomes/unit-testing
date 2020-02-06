using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTest
{
    [TestFixture]
    public class FizzBuzzTests
    {
        [Test]
        public void GetOutput_InputDivisibleBy3_ReturnFizz()
        {
            var result = FizzBuzz.GetOutput(3);

            Assert.That(result, Is.EqualTo("Fizz").IgnoreCase);

        }

        [Test]
        public void GetOutput_InputDivisibleBy5_ReturnBuzz()
        {
            var result = FizzBuzz.GetOutput(5);

            Assert.That(result, Is.EqualTo("Buzz").IgnoreCase);

        }

        [Test]
        public void GetOutput_InputDivisibleBy3And5_ReturnFizzBuzz()
        {
            var result = FizzBuzz.GetOutput(15);

            Assert.That(result, Is.EqualTo("FizzBuzz").IgnoreCase);

        }

        [Test]
        public void GetOutput_InputNotDivisibleBy3Or5_ReturnFizzBuzz()
        {
            var result = FizzBuzz.GetOutput(1);

            Assert.That(result, Is.EqualTo("1").IgnoreCase);

        }

        [Test]
        [TestCase(3, "Fizz")]
        [TestCase(5, "Buzz")]
        [TestCase(15, "FizzBuzz")]
        [TestCase(1, "1")]
        public void GetOutput_WhenCalled_ReturnFizzOrBuzzOrFizzBuzz(int number, string expectedResult)
        {
            var result = FizzBuzz.GetOutput(number);

            Assert.That(result, Is.EqualTo(expectedResult).IgnoreCase);

        }
    }
}
