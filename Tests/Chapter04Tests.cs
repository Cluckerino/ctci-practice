using System;
using System.Linq;
using NUnit.Framework;
using Problems;
using IntNode = Problems.BinaryTreeNode<int>;

namespace Tests
{
    [TestFixture]
    public class Chapter04Tests
    {
        private IntNode testTree;

        [Test]
        public void BinaryTreeTest()
        {
            Console.WriteLine("Tree structure:");
            Console.WriteLine(testTree);

            Assert.That(testTree.GetLeftmost().Value, Is.EqualTo(2));
            Assert.That(testTree.GetRightmost().Value, Is.EqualTo(20));
        }

        /// <summary>
        /// Create example tree to test stuff.
        /// </summary>
        public IntNode CreateExampleTree()
        {
            var exTree = new IntNode(8)
            {
                LVal = 4,
                RVal = 10
            };
            exTree.L.LVal = 2;
            exTree.L.RVal = 6;
            exTree.R.RVal = 20;
            return exTree;
        }

        /// <summary>
        /// Create the test fixture example tree.
        /// </summary>
        [SetUp]
        public void SetupExampleTree()
        {
            testTree = CreateExampleTree();
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
    }
}