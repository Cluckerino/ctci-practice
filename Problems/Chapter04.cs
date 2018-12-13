using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problems
{
    public static class Chapter04
    {
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

    /// <summary>
    /// Graph where values are unique.
    /// </summary>
    /// <typeparam name="T">The node's value type.</typeparam>
    public class SetGraph<T>
    {
        Dictionary<T, GraphNode<T>> nodeDict = new Dictionary<T, GraphNode<T>>();

        /// <summary>
        /// The dictionary of nodes.
        /// </summary>
        public IReadOnlyDictionary<T, GraphNode<T>> NodeDict => nodeDict;

        /// <summary>
        /// The collection of nodes.
        /// </summary>
        public IEnumerable<GraphNode<T>> Nodes => nodeDict.Values;

        /// <summary>
        /// Retrieves the node with value T.
        /// </summary>
        public GraphNode<T> this [T value] => nodeDict[value];

        /// <summary>
        /// Create nodes for the given values and add them to this graph.
        /// </summary>
        /// <param name="values">The values to add.</param>
        public void AddRange(IEnumerable<T> values)
        {
            foreach (var value in values)
                nodeDict.Add(value, new GraphNode<T>(value));
        }

        /// <summary>
        /// Looks up nodes/creates nodes based on values and attaches nodes tp parent.
        /// </summary>
        /// <param name="parent">Parent node value.</param>
        /// <param name="children">Values of children to add.</param>
        public void SetChildren(T parent, params T[] children)
        {
            if (!nodeDict.TryGetValue(parent, out var parentNode))
            {
                parentNode = new GraphNode<T>(parent);
                nodeDict.Add(parent, parentNode);
            }

            foreach (var child in children)
            {
                if (!nodeDict.TryGetValue(child, out var childNode))
                {
                    childNode = new GraphNode<T>(child);
                    nodeDict.Add(child, childNode);
                }
                parentNode.AddChild(childNode);
            }
        }
    }

    /// <summary>
    /// Represents a node.
    /// </summary>
    /// <typeparam name="T">The node's value type.</typeparam>
    public class GraphNode<T>
    {
        private readonly T value;

        private readonly HashSet<GraphNode<T>> children = new HashSet<GraphNode<T>>();

        /// <summary>
        /// Create a new node with the given value.
        /// </summary>
        public GraphNode(T value)
        {
            this.value = value;
        }

        /// <summary>
        /// This node's children.
        /// </summary>
        public ISet<GraphNode<T>> Children => children;

        /// <summary>
        /// This node's value.
        /// </summary>
        public T Value => value;

        /// <summary>
        /// Add a child to this node.
        /// </summary>
        /// <param name="node">The node to add.</param>
        public void AddChild(GraphNode<T> node)
        {
            children.Add(node);
        }

        /// <summary>
        /// Create a string of this node and it's children.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Join(" -> ", children
                .Select(n => n.ValStr)
                .Prepend(ValStr));
        }

        /// <summary>
        /// This node's value as a strimg.
        /// </summary>
        public string ValStr => value?.ToString() ?? "null";
    }

    /// <summary>
    /// Simple binary tree node implementation.
    /// </summary>
    public class BinaryTreeNode<T>
    {
        /// <summary>
        /// Create a node with the default value.
        /// </summary>
        public BinaryTreeNode() { }

        /// <summary>
        /// Create a node with the given value.
        /// </summary>
        /// <param name="value">This node's value.</param>
        public BinaryTreeNode(T value)
        {
            Value = value;
        }

        /// <summary>
        /// Left side node.
        /// </summary>
        public BinaryTreeNode<T> L { get; set; }

        /// <summary>
        /// Shortcut to L's value. Creates node when setting.
        /// </summary>
        public T LVal
        {
            get => L is null ? default(T) : L.Value;
            set
            {
                if (L is null) AddL(value);
                else L.Value = value;
            }
        }

        /// <summary>
        /// Right side node.
        /// </summary>
        public BinaryTreeNode<T> R { get; set; }

        /// <summary>
        /// Shortcut to R's value. Creates node when setting.
        /// </summary>
        public T RVal
        {
            get => R is null ? default(T) : R.Value;
            set
            {
                if (R is null) AddR(value);
                else R.Value = value;
            }
        }

        /// <summary>
        /// Content of this node.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Add an item to the left. Returns the created node.
        /// </summary>
        /// <param name="item">The item to add to the left.</param>
        /// <returns>The created node.</returns>
        public BinaryTreeNode<T> AddL(T item)
        {
            L = new BinaryTreeNode<T>(item);
            return L;
        }

        /// <summary>
        /// Add an item to the right. Returns the created node.
        /// </summary>
        /// <param name="item">The item to add to the right.</param>
        /// <returns>The created node.</returns>
        public BinaryTreeNode<T> AddR(T item)
        {
            R = new BinaryTreeNode<T>(item);
            return R;
        }

        /// <summary>
        /// Gets the leftmost node from the tree.
        /// </summary>
        public BinaryTreeNode<T> GetLeftmost()
        {
            var leftmost = this;
            while (!(leftmost.L is null))
                leftmost = leftmost.L;
            return leftmost;
        }

        /// <summary>
        /// Gets the rightmost node from the tree.
        /// </summary>
        public BinaryTreeNode<T> GetRightmost()
        {
            var rightmost = this;
            while (!(rightmost.R is null))
                rightmost = rightmost.R;
            return rightmost;
        }

        /// <summary>
        /// Create a string with this node as the base.
        /// </summary>
        public override string ToString()
        {
            var sb = new StringBuilder();

            AppendNodeString(this, sb, 0, "H: ");

            return sb.ToString();
        }

        /// <summary>
        /// Recursively drills down the tree and appends strings.
        /// </summary>
        private static void AppendNodeString(BinaryTreeNode<T> node, StringBuilder sb, int indentCount, string prefix)
        {
            // Base case: return if we hit a non-existent node.
            if (node == null) return;

            var indentString = new string(' ', indentCount * 2);

            // Drill left
            AppendNodeString(node.L, sb, indentCount + 1, "L: ");

            // Print this node
            sb.Append(indentString).Append(prefix)
                .AppendLine(node.Value?.ToString() ?? "null");

            // Drill right
            AppendNodeString(node.R, sb, indentCount + 1, "R: ");
        }
    }
}