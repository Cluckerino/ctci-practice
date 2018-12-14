using System.Collections.Generic;
using NUnit.Framework;
using Problems;

namespace Tests
{
    [TestFixture]
    public class Chapter17Tests
    {
        private Dictionary<int, int[]> sparseSimDocs;

        [SetUp]
        public void SetupInputs()
        {
            sparseSimDocs = CreateSpareSimilarityDocs();
        }

        [TestCase(13, 16, 0.25)]
        [TestCase(13, 19, 0.1)]
        [TestCase(19, 24, 0.14285714285714286)]
        public void T26SparseSimilarity(int docAId, int docBId, double expected)
        {
            var docA = sparseSimDocs[docAId];
            var docB = sparseSimDocs[docBId];
            var actual = Chapter17.P26SparseSimilarity(docA, docB);
            Assert.That(actual, Is.EqualTo(expected).Within(1E-12));
        }

        /// <summary>
        /// Create the docs used in 17.26.
        /// </summary>
        private Dictionary<int, int[]> CreateSpareSimilarityDocs()
        {
            return new Dictionary<int, int[]>
            {
                // Random comment to get linter to behave.
                { 13, new int[] { 14, 15, 100, 9, 3 } },
                { 16, new int[] { 32, 1, 9, 3, 5 } },
                { 19, new int[] { 15, 29, 2, 6, 8, 7 } },
                { 24, new int[] { 7, 10 } }
            };
        }
    }
}