using System.Collections.Generic;

namespace Problems
{
    public static class Chapter01
    {
        /// <summary>
        /// Test if a string has all unique characters.
        /// </summary>
        public static bool P01IsUnique(string input)
        {
            var uniqueChars = new HashSet<char>();
            // O(n) iteration.
            foreach (var ch in input)
            {
                // O(1) lookup
                if (uniqueChars.Contains(ch)) return false;
                uniqueChars.Add(ch);
            }
            return true;
        }

        /// <summary>
        /// Are the two inputs one deletion/addition/change away from each other?
        /// </summary>
        public static bool P05OneAway(string input1, string input2)
        {
            if (input1.Length == input2.Length)
                return P05OneAwaySingleReplace(input1, input2);
            else if (input1.Length == input2.Length + 1)
                return P05OneAwaySingleShift(input1, input2);
            else if (input1.Length + 1 == input2.Length)
                return P05OneAwaySingleShift(input2, input1);

            return false;
        }

        /// <summary>
        /// Checks strings of equal length for a single change.
        /// </summary>
        private static bool P05OneAwaySingleReplace(string input1, string input2)
        {
            var firstDiff = false;
            for (int i = 0; i < input1.Length; i++)
            {
                if (input1[i] == input2[i]) continue;

                if (firstDiff) return false;

                firstDiff = true;
            }
            return true;
        }

        /// <summary>
        /// Checks if strings are different by a single insertion/deletion.
        /// </summary>
        private static bool P05OneAwaySingleShift(string longer, string shorter)
        {
            int li = 0, si = 0;
            var firstDiff = false;
            while (si < shorter.Length)
            {
                if (longer[li] == shorter[si])
                {
                    ++li;
                    ++si;
                    continue;
                }

                if (firstDiff) return false;

                firstDiff = true;
                ++li;
            }
            return true;
        }
    }
}