using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public static class Utilities
    {
        private static Random rng = new Random();

        /// <summary>
        /// Create a binary string of this number.
        /// </summary>
        public static string AsBinary(this int num) => Convert.ToString(num, 2);

        /// <summary>
        /// Creates a searchable array that will also provide a member and non-member.
        /// </summary>
        /// <param name="member">Will be set to a member in the array.</param>
        /// <param name="nonMember">Will be set to a non-member.</param>
        /// <returns>The searchable array.</returns>
        public static int[] CreateSearchableArray(out int member, out int nonMember)
        {
            const int min = 0;
            const int max = 35;
            const int elementMin = 2;
            const int elementMax = 10;
            var lowerSectionBound = rng.Next(min + 3, max - 3);
            var upperSectionBound = rng.Next(lowerSectionBound + 1, max);
            nonMember = rng.Next(lowerSectionBound, upperSectionBound);

            var lowerList = Utilities.DrawRandom(rng.Next(elementMin, elementMax),
                min, nonMember);
            var upperList = Utilities.DrawRandom(rng.Next(elementMin, elementMax),
                nonMember + 1, max);

            var fullArray = lowerList
                .Concat(upperList)
                .OrderBy(i => i)
                .ToArray();

            member = fullArray[rng.Next(fullArray.Length)];

            return fullArray;
        }

        /// <summary>
        /// Create an array of random numbers.
        /// </summary>
        /// <param name="count">Number of items in the array.</param>
        /// <param name="min">Inclusive min of random numbers to pick.</param>
        /// <param name="max">Exclusive max of random numbers to pick.</param>
        /// <returns>A list containing random numbers.</returns>
        public static List<int> DrawRandom(int count = 10, int min = 0, int max = 20)
        {
            return Enumerable.Repeat(0, count)
                .Select(_ => rng.Next(min, max))
                .ToList();
        }

        /// <summary>
        /// Creates a string from the given sequence.
        /// </summary>
        public static string Stringify<T>(this IEnumerable<T> sequence) => $"[{string.Join(", ", sequence)}]";
    }
}