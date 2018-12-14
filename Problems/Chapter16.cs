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
            return 0;
        }
    }
}