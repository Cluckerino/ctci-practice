using System.Collections.Generic;
using System.Linq;

namespace Common
{
    /// <summary>
    /// Represents a node.
    /// </summary>
    /// <typeparam name="T">The node's value type.</typeparam>
    public class GraphNode<T>
    {
        private readonly HashSet<GraphNode<T>> children = new HashSet<GraphNode<T>>();

        /// <summary>
        /// Create a new node with the given value.
        /// </summary>
        public GraphNode(T value)
        {
            Value = value;
        }

        /// <summary>
        /// This node's children.
        /// </summary>
        public ISet<GraphNode<T>> Children => children;

        /// <summary>
        /// This node's value as a strimg.
        /// </summary>
        public string ValStr => Value?.ToString() ?? "null";

        /// <summary>
        /// This node's value.
        /// </summary>
        public T Value { get; }

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
    }
}