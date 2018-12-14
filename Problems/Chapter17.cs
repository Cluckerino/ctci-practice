using System;
using System.Linq;

namespace Problems
{
    public static class Chapter17
    {
        /// <summary>
        /// Given two sparse documents with distinct "words" (represented as integers), find
        /// similairy = intersection / union.
        /// </summary>
        public static double P26SparseSimilarity(int[] docA, int[] docB)
        {
            var hashA = docA.ToHashSet();
            var union = docA.Length;
            var intersect = 0;

            foreach (var b in docB)
            {
                // If in A, it's already been counted in union, so just increment intersect.
                if (hashA.Contains(b)) intersect++;
                // Not found in a, so increment union.
                else union++;
            }

            return Convert.ToDouble(intersect) / union;
        }
    }
}