using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;
using Math = TestNinja.Fundamentals.Math;

namespace TestNinja.UnitTest
{
    [TestFixture]
    public class MathTests
    {
        private Math _math;
        // SetUp
        //TearDown
        [SetUp]
        public void SetUP()
        {
            /* SetUp executa antes do test */
            _math = new Math();
        }

        [TearDown]
        public void TearDown()
        {
            /* SetUp executa depois do teste. Como um dispose */
            _math = null;
        }

        [Test]
        [TestCase(1, 2, 3)]
        public void Add_WhenCalled_ReturnSumOfArguments(int a, int b, int expectedResult)
        {
            // arrange
            // act
            var result = _math.Add(a, b);
            // assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [Ignore("It was replaced by Max_WhenCalled_ReturnTheGreaterArgument")]
        public void Max_FirstArgumentIsGreater_ReturnTheFirstArgument()
        {
            var result = _math.Max(2, 1);

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        [Ignore("It was replaced by Max_WhenCalled_ReturnTheGreaterArgument")]
        public void Max_SecondArgumentIsGreater_ReturnTheSecondtArgument()
        {
            var result = _math.Max(1, 2);

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        [Ignore("It was replaced by Max_WhenCalled_ReturnTheGreaterArgument")]
        public void Max_ArgumentsAreEqual_ReturnTheSameArgument()
        {
            var result = _math.Max(1, 1);

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        [TestCase(2, 1, 2)]
        [TestCase(1, 2, 2)]
        [TestCase(1, 1, 1)]
        /* Parameterized Tests */
        public void Max_WhenCalled_ReturnTheGreaterArgument(int a, int b, int expectedResult)
        {
            var result = _math.Max(a, b);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetOddNumbers_LimitIsGreaterThenZero()
        {
            var result = _math.GetOddNumbers(5);
            /* * Testando Arrays e Collections *
            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count(), Is.EqualTo(3));
            
            Assert.That(result, Does.Contain(1));
            Assert.That(result, Does.Contain(3));
            Assert.That(result, Does.Contain(5));
            */

            Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5 }));
        }
    }
}
