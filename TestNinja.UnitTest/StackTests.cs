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
    public class StackTests
    {
        [Test]
        public void Count_EmptyStack_ReturnZero()
        {
            var stack = new Fundamentals.Stack<string>();

            Assert.That(stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Push_InputIsNull_ThrowArgumentNullException()
        {
            var stack = new Fundamentals.Stack<string>();

            Assert.That(() => stack.Push(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Push_InputIsValid_AddObjectToTheStack()
        {
            var stack = new Fundamentals.Stack<string>();
            stack.Push("a");

            Assert.That(stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Pop_EmptyStack_ThrowInvalidOperationException()
        {
            var stack = new Fundamentals.Stack<string>();

            Assert.That(() => stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Pop_StackWithObjects_ReturnObjectOnTheTop()
        {
            // arrange
            var stack = new Fundamentals.Stack<string>();
            stack.Push("a"); stack.Push("b"); stack.Push("c");
            
            // act
            var result = stack.Pop();
            
            //assert
            Assert.That(result, Is.EqualTo("c"));
        }

        [Test]
        public void Pop_StackWithObject_RemoveObjectOnTheTop()
        {
            // arrange
            var stack = new Fundamentals.Stack<string>();
            stack.Push("a"); stack.Push("b"); stack.Push("c");

            // act
            stack.Pop();

            //assert
            Assert.That(stack.Count, Is.EqualTo(2));
        }

        [Test]
        public void Peek_EmptyStack_ThrowInvalidOperationException()
        {
            var stack = new Fundamentals.Stack<string>();

            Assert.That(() => stack.Peek(), Throws.InvalidOperationException);
        }

        public void Peek_StackWithObjects_ReturnObjectOnTheTop()
        {
            // arrange
            var stack = new Fundamentals.Stack<string>();
            stack.Push("a"); stack.Push("b"); stack.Push("c");

            // act
            var result = stack.Peek();

            //assert
            Assert.That(result, Is.EqualTo("c"));
        }

        [Test]
        public void Peek_StackWithObject_DoesNotRemoveObjectOnTheTop()
        {
            // arrange
            var stack = new Fundamentals.Stack<string>();
            stack.Push("a"); stack.Push("b"); stack.Push("c");

            // act
            stack.Peek();

            //assert
            Assert.That(stack.Count, Is.EqualTo(3));
        }

    }
}
