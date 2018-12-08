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
    }
}