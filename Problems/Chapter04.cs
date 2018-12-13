using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace Problems
{
    public static class Chapter04
    {
        /// <summary>
        /// Do a DFS starting from the given node. Return the order of the search as a queue.
        /// </summary>
        public static Queue<GraphNode<int>> P00DepthFirst(SetGraph<int> graph, int initialNode)
        {
            return null;
        }

        /// <summary>
        /// Check if this tree is balanced (i.e. heightL and heightR within 1).
        /// </summary>
        public static bool P04CheckBalanced<T>(BinaryTreeNode<T> head)
        {
            if (head.L is null && head.R is null) return true;
            var lHeight = head.L is null ?
                0 :
                P04RecursiveGetHeight(head.L, 1);

            var rHeight = head.R is null ?
                0 :
                P04RecursiveGetHeight(head.R, 1);

            Console.WriteLine($"Heights - L: {lHeight}, R: {rHeight}");
            return Math.Abs(lHeight - rHeight) <= 1;
        }

        /// <summary>
        /// Recursively drills down the nodes, incrementing height and finding the max.
        /// </summary>
        public static int P04RecursiveGetHeight<T>(BinaryTreeNode<T> currentNode, int currentHeight)
        {
            if (currentNode.L is null && currentNode.R is null)
                return currentHeight;

            var lHeight = currentNode.L is null ?
                currentHeight :
                P04RecursiveGetHeight(currentNode.L, currentHeight + 1);
            var rHeight = currentNode.R is null ?
                currentHeight :
                P04RecursiveGetHeight(currentNode.R, currentHeight + 1);
            return Math.Max(lHeight, rHeight);
        }

        /// <summary>
        /// Find a build order that will work for this graph. In tuple, Item2 is dependent on Item1.
        /// </summary>
        public static List<char> P07BuildOrder(char[] projects, Tuple<char, char>[] dependencies)
        {
            // Create map of nodes;
            var nodeDict = projects
                .ToDictionary(c => c, c => new P07Node(c));

            foreach (var depTuple in dependencies)
            {
                var node = nodeDict[depTuple.Item2];
                var dep = nodeDict[depTuple.Item1];
                node.Dependencies.Add(dep);
            }

            var buildList = new List<char>();

            // Strategy: Iterate through nodes multiple times, finding indepdent nodes.
            // Dependent nodes become independent if thier depenencies are set to build.
            while (nodeDict.Count > 0)
            {
                var indeps = nodeDict
                    .Values
                    .Where(n => n.IsIndependent)
                    .ToList();

                if (indeps.Count == 0)
                    throw new InvalidOperationException("Found no independents: circular dependency?");

                foreach (var indep in indeps)
                {
                    indep.SetToBuild = true;
                    buildList.Add(indep.Name);
                    nodeDict.Remove(indep.Name);
                }
            }

            return buildList;
        }

        /// <summary>
        /// Represents a project and it's dependency, and holds a state if it's queued.
        /// </summary>
        public class P07Node
        {
            /// <summary>
            /// Create a node with the given name.
            /// </summary>
            public P07Node(char name)
            {
                Name = name;
            }

            /// <summary>
            /// List of dependencies.
            /// </summary>
            public List<P07Node> Dependencies { get; } = new List<P07Node>();

            /// <summary>
            /// Trims the dependency list and returns true if this node is now independent.
            /// </summary>
            public bool IsIndependent
            {
                get
                {
                    if (Dependencies.Count == 0) return true;
                    return Dependencies.All(d => d.SetToBuild);
                }
            }

            /// <summary>
            /// The name of the node/project.
            /// </summary>
            public char Name { get; }

            /// <summary>
            /// Set to true if this project's been added to the list and its dependency has been resolved.
            /// </summary>
            public bool SetToBuild { get; set; }
        }
    }
}