using System.Text;

namespace Common
{
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