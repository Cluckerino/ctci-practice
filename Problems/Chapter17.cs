using System;
using System.Collections.Generic;
using System.Linq;

namespace Problems
{
    public static class Chapter17
    {
        /// <summary>
        /// Creates a string ID from the given pair to be used as a key. Uses "min,max" for
        /// consistency.
        /// </summary>
        public static string IdPair(int a, int b)
        {
            return $"{Math.Min(a, b)},{Math.Max(a, b)}";
        }

        /// <summary>
        /// Given two sparse documents with distinct "words" (represented as integers), find
        /// similairy = intersection / union.
        /// </summary>
        public static double P26SparseSimilarity(int[] docA, int[] docB)
        {
            var hashA = docA.ToHashSet();

            var intersect = P26SparseSimilarity(hashA, docB, out var union);
            return Convert.ToDouble(intersect) / union;
        }

        /// <summary>
        /// Given two sparse documents with distinct "words" (represented as integers), find the
        /// intersection (returned), and the union (passed as out var). docA is passed as a cached
        /// hashes.
        /// </summary>
        /// <param name="hashA">Hash table of docA.</param>
        /// <param name="docB">DocB.</param>
        /// <param name="union">Will be set to the final union of both docs.</param>
        /// <returns>The intersect of both docs.</returns>
        public static int P26SparseSimilarity(HashSet<int> hashA, int[] docB, out int union)
        {
            union = hashA.Count;
            var intersect = 0;

            foreach (var b in docB)
            {
                // If in A, it's already been counted in union, so just increment intersect.
                if (hashA.Contains(b)) intersect++;
                // Not found in a, so increment union.
                else union++;
            }

            return intersect;
        }

        /// <summary>
        /// Process the entire list of documents and calculate similarities for pairs with
        /// similarities.
        /// </summary>
        /// <param name="allDocs">Dicionary of all docs.</param>
        /// <returns>Results as {doc-pair string, similarity}.</returns>
        public static Dictionary<string, double> P26SparseSimilarityAllDocs(Dictionary<int, int[]> allDocs)
        {
            var results = new Dictionary<string, double>();

            var allDocsList = allDocs.ToList();
            for (int aI = 0; aI < allDocsList.Count; aI++)
            {
                // Key-value (ID, doc) for A.
                var kvA = allDocsList[aI];
                // Get docA and hash.
                var hashA = kvA.Value.ToHashSet();

                for (int bI = aI + 1; bI < allDocsList.Count; bI++)
                {
                    // Key-value (ID, doc) for B.
                    var kvB = allDocsList[bI];
                    var docB = kvB.Value;

                    var intersect = P26SparseSimilarity(hashA, docB, out var union);
                    // Don't bother with processing results if the two have nothing in common.
                    if (intersect == 0) continue;

                    // Append result
                    results.Add(IdPair(kvA.Key, kvB.Key), Convert.ToDouble(intersect) / union);
                }
            }

            return results;
        }
    }
}