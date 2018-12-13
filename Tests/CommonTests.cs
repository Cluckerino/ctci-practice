using System;
using Common;
using NUnit.Framework;
using IntNode = Common.BinaryTreeNode<int>;

namespace Tests
{
    [TestFixture]
    public class CommonTests
    {
        private IntNode testTree;

        /// <summary>
        /// Create example tree to test stuff.
        /// </summary>
        public static IntNode CreateExampleTree()
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

        [Test]
        public void BinaryTreeTest()
        {
            Console.WriteLine("Tree structure:");
            Console.WriteLine(testTree);

            Assert.That(testTree.GetLeftmost().Value, Is.EqualTo(2));
            Assert.That(testTree.GetRightmost().Value, Is.EqualTo(20));
        }

        [Test]
        public void GraphTest()
        {
            var graph = new SetGraph<int>();
            graph.SetChildren(4, 6);
            graph.SetChildren(5, 4);
            graph.SetChildren(6, 5);

            graph.SetChildren(2, 0, 3);
            graph.SetChildren(3, 2);
            graph.SetChildren(1, 2);
            graph.SetChildren(0, 1);

            foreach (var node in graph.Nodes)
                Console.WriteLine(node);

            var node0 = graph[0];
            var node2 = graph[2];
            var node3 = graph[3];

            Assert.That(node2.Children, Has.Member(node3));
            Assert.That(node2.Children, Has.Member(node0));

            Assert.That(node3.Children, Has.Member(node2));
            Assert.That(node0.Children, Has.No.Member(node2));
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