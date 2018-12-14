using System;
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
    }
}