using System.Collections.Generic;

namespace Common
{
    /// <summary>
    /// Graph where values are unique.
    /// </summary>
    /// <typeparam name="T">The node's value type.</typeparam>
    public class SetGraph<T>
    {
        private Dictionary<T, GraphNode<T>> nodeDict = new Dictionary<T, GraphNode<T>>();

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
}