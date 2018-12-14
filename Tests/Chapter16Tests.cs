using System;
using Common;
using NUnit.Framework;
using Problems;

namespace Tests
{
    [TestFixture]
    public class Chapter16Tests
    {
        [Test]
        public void T01InPlaceSwap()
        {
            var rng = new Random();

            var a = rng.Next(int.MinValue / 2, int.MaxValue / 2);
            var b = rng.Next(int.MinValue / 2, int.MaxValue / 2);

            var expectedA = b;
            var expectedB = a;

            Chapter16.P01InPlaceSwap(ref a, ref b);

            Assert.That(a, Is.EqualTo(expectedA));
            Assert.That(b, Is.EqualTo(expectedB));
        }

        [Test]
        public void T06SmallestDifference()
        {
            var a = new [] { 1, 3, 15, 11, 2 };
            var b = new [] { 23, 127, 235, 19, 8 };
            var expected = 3;

            var actual = Chapter16.P06SmallestDifference(a, b);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(1023, 1022)]
        [TestCase(1022, 1023)]
        [TestCase(512, 513)]
        [TestCase(513, 512)]
        [TestCase(1022, 513)]
        [TestCase(513, 1022)]
        public void T07NumberMax(int a, int b)
        {
            var expected = Math.Max(a, b);

            Console.WriteLine($"a: {a} {a.AsBinary()}");
            Console.WriteLine($"b: {b} {b.AsBinary()}");

            var actual = Chapter16.P07NumberMax(a, b);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}