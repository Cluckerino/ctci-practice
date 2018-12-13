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
    }
}