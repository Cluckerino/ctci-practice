using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace Problems
{
    public static class Chapter10
    {
        /// <summary>
        /// Does a binary search to find x in the given sorted array. Returns true and sets the param
        /// if found.
        /// </summary>
        public static bool P00RecursiveBinarySearch(int[] array, int value, out int indexOut)
        {
            indexOut = 0;
            return P00RecursiveBinarySearch(array, value, 0, array.Length - 1, ref indexOut);
        }

        /// <summary>
        /// Assuming that a and be are sorted an a has enough buffer at the end to hold b, merge the
        /// two in sorted order.
        /// </summary>
        public static int[] P01SortedMerge(int[] a, int aOrigLength, int[] b)
        {
            // Do a reverse zig-zag search. Zig-zag touches each element at most twice
            var aInd = aOrigLength - 1;
            var bInd = b.Length - 1;
            var lastInd = aOrigLength + b.Length - 1;
            var aDone = false;
            var bDone = false;
            var aVal = a[aInd];
            var bVal = b[bInd];
            for (var i = lastInd; i >= 0; i--)
            {
                if (aDone && bDone) throw new InvalidOperationException("How'd we get here????");

                if (aDone || (!bDone && bVal > aVal))
                {
                    a[i] = bVal;
                    bInd--;
                    // Decrementing past 0 = done.
                    bDone = bInd < 0;
                    // Set next value
                    if (!bDone)
                        bVal = b[bInd];
                }
                else
                {
                    a[i] = aVal;
                    aInd--;
                    // Decrementing past 0 = done.
                    aDone = aInd < 0;
                    if (!aDone)
                        aVal = a[aInd];
                }
            }
            return a;
        }

        /// <summary>
        /// Fint the index given number in the sorted input array, given that the array has been
        /// rotated an unknown number of times.
        /// </summary>
        public static bool P03RotatedArray(int[] array, int value, out int indexOut)
        {
            indexOut = 0;
            return P03RotatedArray(array, value, 0, array.Length - 1, ref indexOut);
        }

        /// <summary>
        /// The recursive call.
        /// </summary>
        private static bool P00RecursiveBinarySearch(int[] array, int value, int left, int right, ref int indexOut)
        {
            if (left > right) return false;

            var mid = (left + right) / 2;
            if (array[mid] < value)
                return P00RecursiveBinarySearch(array, value, mid + 1, right, ref indexOut);
            else if (array[mid] > value)
                return P00RecursiveBinarySearch(array, value, left, mid - 1, ref indexOut);
            else
            {
                indexOut = mid;
                return true;
            }
        }

        /// <summary>
        /// The recursive call.
        /// </summary>
        private static bool P03RotatedArray(int[] array, int value, int left, int right, ref int indexOut)
        {
            if (left > right) return false;

            var mid = (left + right) / 2;
            var mVal = array[mid];

            // Got em
            if (mVal == value)
            {
                indexOut = mid;
                return true;
            }

            // Don't got em, keep searching.
            var lVal = array[left];
            var rVal = array[right];

            // If right <= mid, pivot point might be on the right, so search anyway. Otherwise, right
            // is sorted, so just use regular binary search logic and check if the value could be
            // there.
            var searchRight = rVal <= mVal || mVal < value;
            // Same logic for the left.
            var searchLeft = lVal >= mVal || mVal > value;

            if (searchRight)
            {
                var foundRight = P03RotatedArray(array, value, mid + 1, right, ref indexOut);
                // We found it alredy
                if (foundRight) return true;
            }

            if (searchLeft)
            {
                return P03RotatedArray(array, value, left, mid - 1, ref indexOut);
            }

            return false;
        }

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