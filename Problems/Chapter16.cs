using System;

namespace Problems
{
    public static class Chapter16
    {
        /// <summary>
        /// Swap 'em without temp variables.
        /// </summary>
        public static void P01InPlaceSwap(ref int a, ref int b)
        {
            if (a == b) return;
            a = b - a;
            b = b - a;
            a = a + b;
        }

        /// <summary>
        /// Find the pair of values, one from each array, with the smallest difference and return the
        /// difference.
        /// </summary>
        public static int P06SmallestDifference(int[] a, int[] b)
        {
            Array.Sort(a);
            Array.Sort(b);

            var aInd = a.Length - 1;
            var bInd = b.Length - 1;
            // Initialize to something high.
            var minDiff = int.MaxValue;

            while (aInd >= 0 && bInd >= 0)
            {
                var aVal = a[aInd];
                var bVal = b[bInd];
                var curDiff = Math.Abs(bVal - aVal);
                if (curDiff < minDiff)
                {
                    minDiff = curDiff;
                }

                if (aVal > bVal) aInd--;
                else bInd--;
            }

            return minDiff;
        }
    }
}