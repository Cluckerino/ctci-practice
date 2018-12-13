using System;
using System.Linq;
using Common;
using NUnit.Framework;
using Problems;
using IntNode = Common.BinaryTreeNode<int>;

namespace Tests
{
    [TestFixture]
    public class Chapter04Tests
    {
        private SetGraph<int> testGraph;
        private IntNode testTree;

        public static SetGraph<int> CreateExampleGraph()
        {
            var graph = new SetGraph<int>();
            graph.SetChildren(0, 1, 4, 5);
            graph.SetChildren(1, 3, 4);
            graph.SetChildren(2, 1);
            graph.SetChildren(3, 2, 4);
            return graph;
        }

        /// <summary>
        /// Create the test fixture example tree.
        /// </summary>
        [SetUp]
        public void SetupTestGraphs()
        {
            testTree = CommonTests.CreateExampleTree();
            testGraph = CreateExampleGraph();
        }

        [Test]
        public void T00BreadthFirst()
        {
            var actualNodes = Chapter04.P00BreadthFirst(testGraph, 0);
            var actual = actualNodes
                .Select(n => n.Value)
                .ToList();

            Console.WriteLine(actual.Stringify());

            // First item had better be 0.
            Assert.That(actual[0], Is.EqualTo(0));
            // Contents should be the same.
            Assert.That(actualNodes, Is.EquivalentTo(testGraph.Nodes));
        }

        [Test]
        public void T00DepthFirst()
        {
            var actualNodes = Chapter04.P00DepthFirst(testGraph, 0);

            var actual = actualNodes
                .Select(n => n.Value)
                .ToList();

            var index0 = actual.FindIndex(v => v == 0);
            var index3 = actual.FindIndex(v => v == 3);

            Console.WriteLine(actual.Stringify());

            // First item had better be 0.
            Assert.That(actual[0], Is.EqualTo(0));
            // Contents should be the same.
            Assert.That(actualNodes, Is.EquivalentTo(testGraph.Nodes));
            // In the DFS, 0 should drill down to 3.
            Assert.That(index0, Is.LessThan(index3));
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