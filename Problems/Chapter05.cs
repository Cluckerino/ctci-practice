namespace Problems
{
    public static class Chapter05
    {
        /// <summary>
        /// Insert m into n at bits i-j inclusive.
        /// </summary>
        public static int P01Insertion(int n, int m, int i, int j)
        {
            var mask = P01Mask(i, j);

            // Clear all bits of n in the mask.
            var clearedN = n & ~mask;

            // Add shifted m.
            return clearedN + (m << i);
        }

        /// <summary>
        /// Create a bit mask at bits i-j inclusive.
        /// </summary>
        public static int P01Mask(int i, int j)
        {
            // Ex: i = 2, j = 6.

            // Only looking at the first 10 bits here.
            // Create 1s => 11 1111 1111
            int mask = -1;

            // Since bits i-j inclusive is 5 bits, I need to create 5 1s for the mask. Start with 1 => 00 0000 0001
            mask = 1;
            // Shift j - i + 1 = 6 - 2 + 1 = 5 time to move it to the 6th bit. => 00 0010 0000
            mask <<= j - i + 1;
            // Subtract 1 to create a tail of 1s. => 00 0001 1111
            mask -= 1;

            // Shift by i = 2 to move the 1's to the correct position => 00 0111 1100
            return mask << i;
        }
    }
}