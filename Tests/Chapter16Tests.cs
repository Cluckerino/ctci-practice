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
    }
}