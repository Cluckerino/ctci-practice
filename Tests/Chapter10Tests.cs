using System;
using System.Linq;
using Common;
using NUnit.Framework;
using Problems;

namespace Tests
{
    [TestFixture]
    public class Chapter10Tests
    {
        private static Random rng = new Random();

        [Test]
        public void T00CreateCopy()
        {
            var input = Utilities.DrawRandom();
            var expected = input.ToList();
            var actual = Chapter10.P00QuickSort<int>.CreateCopy(input);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void T00QuickSort()
        {
            var input = Utilities.DrawRandom();
            var expected = input.OrderBy(i => i).ToList();
            var actual = Chapter10.P00QuickSort<int>.Sort(input);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void T00RecursiveBinarySearch()
        {
            var array = Utilities.CreateSearchableArray(out var member, out var nonMember);
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
            var aOrig = Utilities.DrawRandom(5, -5, 10).OrderBy(i => i).ToList();
            var b = Utilities.DrawRandom(15, 0, 15).OrderBy(i => i).ToArray();
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

        [Test]
        public void T03RotatedArray()
        {
            var array = Utilities.CreateSearchableArray(out var member, out var nonMember);
            var quarter = array.Length / 4;
            var half = array.Length / 2;
            var pivot = rng.Next(half - quarter, half + quarter);
            array = Enumerable.Range(pivot, array.Length - pivot)
                .Concat(Enumerable.Range(0, pivot))
                .Select(i => array[i])
                .ToArray();

            Console.WriteLine($"Finding member {member} and non-member {nonMember} in:");
            Console.WriteLine(array.Stringify());

            Assume.That(array, Has.Member(member));
            Assume.That(array, Does.Not.Contain(nonMember));

            var actual = Chapter10.P03RotatedArray(array, member, out var index);
            Assert.That(actual, Is.True);
            Assert.That(array[index], Is.EqualTo(member));

            actual = Chapter10.P03RotatedArray(array, nonMember, out index);
            Assert.That(actual, Is.False);
        }
    }
}