using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Problems;

namespace Tests
{
    [TestFixture]
    public class Chapter02Tests
    {
        public void T01RemoveDupes(IEnumerable<int> input, IEnumerable<int> expected)
        {
            var inputList = new LinkedList<int>(input);
            var expectedList = new LinkedList<int>(expected);
            Assert.That(Chapter02.P01RemoveDupes(inputList), Is.EqualTo(expectedList));
        }

        [Test]
        public void T01RemoveDupesHasDupes() =>
            T01RemoveDupes(new [] { 3, 4, 5, 6, 3, 3, 5, 6, 7 }, new [] { 3, 4, 5, 6, 7 });

        [Test]
        public void T01RemoveDupesNoDupes() =>
            T01RemoveDupes(new [] { 3, 4, 5, 6, 7 }, new [] { 3, 4, 5, 6, 7 });

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        public void T02KthToLast(int kth)
        {
            var input = new LinkedList<int>(Enumerable.Range(1, 10));
            var expected = 10 - kth;
            Assert.That(Chapter02.P02KthToLast(input, kth), Is.EqualTo(expected));
        }

        public LinkedList<int> T05Listify(int num)
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
        public void T05ListifyTest()
        {
            var input = 14325;
            var expected = new [] { 5, 2, 3, 4, 1 };
            Assert.That(T05Listify(input), Is.EqualTo(expected));
        }

        [TestCase(617, 295, 617 + 295)]
        [TestCase(999, 999, 999 + 999)]
        [TestCase(951, 7632, 951 + 7632)]
        public void T05SumList(int a, int b, int sum)
        {
            var aL = T05Listify(a);
            var bL = T05Listify(b);
            var expected = T05Listify(sum);

            var actual = Chapter02.P05SumLists(aL, bL);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}