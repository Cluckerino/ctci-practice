using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Problems;

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

        [Test]
        public void T00RecursiveBinarySearch()
        {
            var array = CreateSearchable(out var member, out var nonMember);
            Console.WriteLine($"Finding member {member} and non-member {nonMember} in:");
            Console.WriteLine(array.Stringify());

            Assume.That(array, Has.Member(member));
            Assume.That(array, Does.Not.Contain(nonMember));

            var actual = Chapter10.P00RecursiveBinarySearch(array, member, out var index);
            Assert.That(actual, Is.True);
            Assert.That(array[index], Is.EqualTo(member));

            actual = Chapter10.P00RecursiveBinarySearch(array, nonMember, out index);
            Assert.That(actual, Is.False);
        }

        [Test]
        public void T01SortedMerge()
        {
            var aOrig = DrawRandom(5, -5, 10).OrderBy(i => i).ToList();
            var b = DrawRandom(15, 0, 15).OrderBy(i => i).ToArray();
            var expected = aOrig.Concat(b).OrderBy(i => i).ToList();
            Console.WriteLine("Merging the following:");
            Console.WriteLine($" a = {aOrig.Stringify()}");
            Console.WriteLine($" b = {b.Stringify()}");
            var a = aOrig
                .Concat(Enumerable.Repeat(default(int), b.Length))
                .ToArray();

            var actual = Chapter10.P01SortedMerge(a, aOrig.Count, b);

            Console.WriteLine("Result:");
            Console.WriteLine(actual.Stringify());
            Assert.That(actual, Is.EqualTo(expected));
        }

        private int[] CreateSearchable(out int member, out int nonMember)
        {
            const int min = 0;
            const int max = 35;
            const int elementMin = 2;
            const int elementMax = 10;
            var lowerSectionBound = rng.Next(min + 3, max - 3);
            var upperSectionBound = rng.Next(lowerSectionBound + 1, max);
            nonMember = rng.Next(lowerSectionBound, upperSectionBound);

            var lowerList = DrawRandom(rng.Next(elementMin, elementMax),
                min, nonMember);
            var upperList = DrawRandom(rng.Next(elementMin, elementMax),
                nonMember + 1, max);

            var fullArray = lowerList
                .Concat(upperList)
                .OrderBy(i => i)
                .ToArray();

            member = fullArray[rng.Next(fullArray.Length)];

            return fullArray;
        }
    }
}