using NUnit.Framework;
using Problems;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class Chapter02Tests
    {
        public void P01_RemoveDupes(IEnumerable<int> input, IEnumerable<int> expected)
        {
            var inputList = new LinkedList<int>(input);
            var expectedList = new LinkedList<int>(expected);
            Assert.That(Chapter02.P01_RemoveDupes(inputList), Is.EqualTo(expectedList));
        }

        [Test]
        public void P01_RemoveDupes_HasDupes() =>
            P01_RemoveDupes(new[] { 3, 4, 5, 6, 3, 3, 5, 6, 7 }, new[] { 3, 4, 5, 6, 7 });

        [Test]
        public void P01_RemoveDupes_NoDupes() =>
            P01_RemoveDupes(new[] { 3, 4, 5, 6, 7 }, new[] { 3, 4, 5, 6, 7 });

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        public void P02_KthToLast(int kth)
        {
            var input = new LinkedList<int>(Enumerable.Range(1, 10));
            var expected = 10 - kth;
            Assert.That(Chapter02.P02_KthToLast(input, kth), Is.EqualTo(expected));
        }

        public LinkedList<int> P05_Listify(int num)
        {
            var digits = new LinkedList<int>();
            var remaining = num;
            while (remaining > 0)
            {
                remaining = Math.DivRem(remaining, 10, out var digit);
                digits.AddLast(digit);
            }
            return digits;
        }

        [Test]
        public void P05_ListifyTest()
        {
            var input = 14325;
            var expected = new[] { 5, 2, 3, 4, 1 };
            Assert.That(P05_Listify(input), Is.EqualTo(expected));
        }

        [TestCase(617, 295, 617 + 295)]
        public void P05_SumList(int a, int b, int sum)
        {
            var aL = P05_Listify(a);
            var bL = P05_Listify(b);
            var expected = P05_Listify(sum);

            var actual = Chapter02.P05_SumLists(aL, bL);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}