using System;
using System.Collections.Generic;

namespace Problems
{
    public static class Chapter02
    {
        /// <summary>
        /// Remove duplicates from the linked list.
        /// </summary>
        public static LinkedList<int> P01RemoveDupes(LinkedList<int> input)
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
        public static int P02KthToLast(LinkedList<int> singlyInput, int kth)
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

        /// <summary>
        /// Recursive function that adds the digits and populates the answer list.
        /// </summary>
        public static void P05RecursiveAddDigit(LinkedListNode<int> a, LinkedListNode<int> b,
            int carryOver, LinkedList<int> answer)
        {
            // If digit isn't available, use 0.
            var aVal = a?.Value ?? 0;
            var bVal = b?.Value ?? 0;
            // Calculate the sum and store the carryover for next iteration.
            var digitSum = aVal + bVal + carryOver;
            var newCarryOver = Math.DivRem(digitSum, 10, out digitSum);
            answer.AddLast(digitSum);

            var aNext = a?.Next;
            var bNext = b?.Next;
            // Check if we ran out of digits to add.
            if (aNext is null && bNext is null && newCarryOver == 0) return;

            // Recursively add the next digit.
            P05RecursiveAddDigit(aNext, bNext, newCarryOver, answer);
        }

        /// <summary>
        /// Treat the list as digits and add them.
        /// </summary>
        public static LinkedList<int> P05SumLists(LinkedList<int> a, LinkedList<int> b)
        {
            var answer = new LinkedList<int>();
            P05RecursiveAddDigit(a.First, b.First, 0, answer);
            return answer;
        }
    }
}