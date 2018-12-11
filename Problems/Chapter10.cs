using System;
using System.Collections.Generic;
using System.Linq;

namespace Problems
{
    public static class Chapter10
    {
        /// <summary>
        /// Creates a string from the given sequence.
        /// </summary>
        public static string Stringify<T>(this IEnumerable<T> sequence) => $"[{string.Join(", ", sequence)}]";

        /// <summary>
        /// Attempt to implement a quicksort algo.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static class P00QuickSort<T> where T : IComparable<T>
        {
            /// <summary>
            /// Create a copy of this array.
            /// </summary>
            public static List<T> CreateCopy(List<T> list) => CreateCopy(list, 0, list.Count - 1);

            /// <summary>
            /// Noisy quicksorts the given array.
            /// </summary>
            public static List<T> Sort(List<T> list)
            {
                if (list is null) throw new ArgumentNullException(nameof(list));
                Print($"Sorting: {list.Stringify()}");

                var copy = CreateCopy(list);

                Sort(copy, 0, copy.Count - 1);

                Print($"Done sorting: {copy.Stringify()}");

                return copy;
            }

            /// <summary>
            /// Create a copy of a subset of this array.
            /// </summary>
            private static List<T> CreateCopy(List<T> list, int left, int right)
            {
                var length = right - left + 1;
                var copy = list.ToArray();
                list.CopyTo(0, copy, left, length);
                return copy.ToList();
            }

            /// <summary>
            /// Greater than.
            /// </summary>
            private static bool Gt(T lhs, T rhs) => lhs.CompareTo(rhs) > 0;

            /// <summary>
            /// Less than.
            /// </summary>
            private static bool Lt(T lhs, T rhs) => lhs.CompareTo(rhs) < 0;

            /// <summary>
            /// Picks a pivot and does the swapparoni.
            /// </summary>
            private static int Partition(List<T> list, int left, int right)
            {
                // Orig values.
                int oleft = left, oright = right;

                var toPrint = CreateCopy(list, oleft, oright);
                Print($"  Partioning from {left} to {right}: {toPrint.Stringify()}");

                // Pivot index
                var mid = (left + right) / 2;
                var pivot = list[mid];
                Print($"    Pivoting at a[{mid}] = {pivot}");

                while (left <= right)
                {
                    while (Lt(list[left], pivot)) left++;
                    while (Gt(list[right], pivot)) right--;

                    if (left <= right)
                    {
                        Print($"    Swapping a[{left}] = {list[left]} and a[{right}] = {list[right]}");
                        var temp = list[right];
                        list[right] = list[left];
                        list[left] = temp;
                        left++;
                        right--;
                    }
                }

                toPrint = CreateCopy(list, oleft, oright);
                Print($"  Sorted partition: {toPrint.Stringify()}");

                return left;
            }

            /// <summary>
            /// Print to console.
            /// </summary>
            private static void Print(string s) => Console.WriteLine(s);

            /// <summary>
            /// Recursive sort function.
            /// </summary>
            private static void Sort(List<T> list, int left, int right)
            {
                Print(string.Empty);
                Print($"Sorting: {list.Stringify()}");
                Print($"From a[{left}] to a[{right}]");

                var index = Partition(list, left, right);
                Print($"Next boundary?: a[{index}]");

                // Sort left
                if (left < index - 1)
                    Sort(list, left, index - 1);

                // Sort right
                if (index < right)
                    Sort(list, index, right);
            }
        }
    }
}