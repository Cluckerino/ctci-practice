using System;
using NUnit.Framework;
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
    }
}