using System;
using System.Linq;
using NUnit.Framework;
using Problems;
using IntNode = Common.BinaryTreeNode<int>;

namespace Tests
{
    [TestFixture]
    public class Chapter04Tests
    {
        private IntNode testTree;

        /// <summary>
        /// Create the test fixture example tree.
        /// </summary>
        [SetUp]
        public void SetupExampleTree()
        {
            testTree = CommonTests.CreateExampleTree();
        }

        [Test]
        public void T04CheckBalanced()
        {
            // Both should have height 2
            Assert.That(Chapter04.P04CheckBalanced(testTree), Is.True);

            // lHeight = 3, rHeight = 2, balanced.
            testTree.GetLeftmost().LVal = 1;
            Assert.That(Chapter04.P04CheckBalanced(testTree), Is.True);

            // lHeight = 4, rHeight = 2, not balanced.
            testTree.GetLeftmost().LVal = 0;
            Assert.That(Chapter04.P04CheckBalanced(testTree), Is.False);
        }

        [Test]
        public void T07BuildOrder()
        {
            var projects = new [] { 'a', 'b', 'c', 'd', 'e', 'f' };
            var dependencies = new []
            {
                Tuple.Create('a', 'd'),
                Tuple.Create('f', 'b'),
                Tuple.Create('b', 'd'),
                Tuple.Create('f', 'a'),
                Tuple.Create('d', 'c'),
            };

            var actual = Chapter04.P07BuildOrder(projects, dependencies);

            Assert.That(actual, Has.Count.EqualTo(projects.Length));

            // Answer: f & e, a & b, d, c
            var firstTwo = actual.Take(2).ToList();
            Assert.That(firstTwo, Is.EquivalentTo(new [] { 'e', 'f' }));

            var nextTwo = actual.Skip(2).Take(2);
            Assert.That(nextTwo, Is.EquivalentTo(new [] { 'a', 'b' }));

            Assert.That(actual[4], Is.EqualTo('d'));
            Assert.That(actual[5], Is.EqualTo('c'));
        }

        [Test]
        public void T07BuildOrderCircularDependencies()
        {
            var projects = new [] { 'a', 'b', 'c', 'd', 'e', 'f' };
            var dependencies = new []
            {
                Tuple.Create('a', 'd'),
                Tuple.Create('f', 'b'),
                Tuple.Create('b', 'd'),
                Tuple.Create('f', 'a'),
                Tuple.Create('d', 'c'),
                Tuple.Create('c', 'b'),
            };

            // Circular dependency -> d req b, b req c, c req d.

            Assert.That(() => Chapter04.P07BuildOrder(projects, dependencies),
                Throws.Exception.TypeOf<InvalidOperationException>());
        }
    }
}