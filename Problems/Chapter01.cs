using System.Collections.Generic;

namespace Problems
{
    public static class Chapter01
    {
        /// <summary>
        /// Test if a string has all unique characters.
        /// </summary>
        public static bool P01_IsUnique(string input)
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
        public static bool P05_OneAway(string input1, string input2)
        {
            return false;
        }
    }
}