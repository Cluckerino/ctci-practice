using System.Collections.Generic;

namespace Problems
{
    public static class Chapter02
    {
        /// <summary>
        /// Remove duplicates from the linked list.
        /// </summary>
        public static LinkedList<int> P01_RemoveDupes(LinkedList<int> input)
        {
            var uniques = new HashSet<int>();
            var currentNode = input.First;
            while (currentNode != null)
            {
                var nextNode = currentNode.Next;
                if (uniques.Contains(currentNode.Value))
                    input.Remove(currentNode);
                else
                    uniques.Add(currentNode.Value);
                currentNode = nextNode;
            }
            return input;
        }

        /// <summary>
        /// Find the Kth to last element.
        /// </summary>
        /// <param name="singlyInput">Treat this as a singly-linked list.</param>
        /// <param name="kth">Kth element from the tail. Zero-based.</param>
        /// <returns>The kth to last element.</returns>
        public static int P02_KthToLast(LinkedList<int> singlyInput, int kth)
        {
            var headNode = singlyInput.First;
            var kthNode = headNode;
            // Counter to keep track of how far we are from the head node.
            var kthCounter = kth;
            // Check next; we don't need to iterate both pointers if this is the last node.
            while (headNode.Next != null)
            {
                if (kthCounter == 0)
                    kthNode = kthNode.Next;
                else
                    --kthCounter;
                headNode = headNode.Next;
            }
            return kthNode.Value;
        }
    }
}