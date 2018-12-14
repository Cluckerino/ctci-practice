using System.Collections.Generic;

namespace Common
{
    /// <summary>
    /// Handles status of visited nodes.
    /// </summary>
    public class VisitedNodeInfo<T>
    {
        /// <summary>
        /// Visited nodes as a set for fast lookup.
        /// </summary>
        public HashSet<GraphNode<T>> Lookup { get; } = new HashSet<GraphNode<T>>();

        /// <summary>
        /// Visited nodes as a queue to check order.
        /// </summary>
        public Queue<GraphNode<T>> Order { get; } = new Queue<GraphNode<T>>();

        /// <summary>
        /// Checks if this node has been visited.
        /// </summary>
        /// <param name="node">The node to check.</param>
        /// <returns>True if it's already been visited.</returns>
        public bool IsVisited(GraphNode<T> node) => Lookup.Contains(node);

        /// <summary>
        /// Add the node to mark as visited.
        /// </summary>
        /// <param name="node"></param>
        public void MarkVisited(GraphNode<T> node)
        {
            Lookup.Add(node);
            Order.Enqueue(node);
        }
    }
}