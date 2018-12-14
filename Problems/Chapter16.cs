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

        /// <summary>
        /// Find the max of two numbers without using if-else (or ternaries) and comparison operators
        /// (==, &gt;, &lt; etc.)
        /// </summary>
        public static int P07NumberMax(int a, int b)
        {
            // 1 if positive, 0 if negative.
            var signA = ((a >> 31) & 1) ^ 1;
            var signB = ((b >> 31) & 1) ^ 1;

            // 1 if the different, 0 if the same.
            var diffSign = signA ^ signB;

            // 1 if different sign and A is greater. O if B is greater or same sign.
            var aSignGreater = diffSign * signA;

            // Max bit is the comparer 1 for a, 0 for b.
            var maxBit = 0;

            // Need to check each of the 32 bits to see who is bigger. i represents the current bit
            // of interest.
            for (var i = 0; i < 32; i++)
            {
                var bitA = (a >> i) & 1;
                var bitB = (b >> i) & 1;

                // 1 if different, 0 if not.
                var diff = bitA ^ bitB;

                // 1 if different with bitA > bitB (implying a > b for this digit). 0 if the same, or
                // bitB > bitA.
                var pickA = diff * bitA;

                // If diff, will throw away current answer by shifting it away. pickA is only
                // siginificant if set (i.e. diff AND bitA > bitB).
                maxBit = (maxBit >> diff) + pickA;

                // Sign override: if different signs, maxBit should always be A or B. Have to do this
                // 32 times because no flow controls are allowed.
                maxBit = maxBit * (diffSign ^ 1) + aSignGreater;
            }

            return maxBit * a + (maxBit ^ 1) * b;
        }
    }
}