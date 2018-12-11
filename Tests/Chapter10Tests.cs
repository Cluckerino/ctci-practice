using NUnit.Framework;
using Problems;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class Chapter10Tests
    {
        private static Random rng = new Random();

        /// <summary>
        /// Create an array of random numbers.
        /// </summary>
        public static List<int> DrawRandom(int count = 10, int min = 0, int max = 20)
        {
            return Enumerable.Repeat(0, count)
                .Select(_ => rng.Next(min, max))
                .ToList();
        }

        [Test]
        public void T00CreateCopy()
        {
            var input = DrawRandom();
            var expected = input.ToList();
            var actual = Chapter10.P00QuickSort<int>.CreateCopy(input);
            Assert.That(actual, Is.EqualTo(expected));

        }

        [Test]
        public void T00QuickSort()
        {
            var input = DrawRandom();
            var expected = input.OrderBy(i => i).ToList();
            var actual = Chapter10.P00QuickSort<int>.Sort(input);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}