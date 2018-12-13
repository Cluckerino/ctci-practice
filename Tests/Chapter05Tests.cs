using System;
using Common;
using NUnit.Framework;
using Problems;

namespace Tests
{
    [TestFixture]
    public class Chapter05Tests
    {
        [TestCase(0b10000000000, 0b10011, 2, 6, 0b10001001100)]
        [TestCase(0b10001110100, 0b10011, 2, 6, 0b10001001100)]
        [TestCase(0b10000000000, 0b10011, 2, 10, 0b00001001100)]
        [TestCase(0b11111111111, 0b10011, 2, 6, 0b11111001111)]
        public void T01Insertion(int n, int m, int i, int j, int expected)
        {
            int actual = Chapter05.P01Insertion(n, m, i, j);

            Console.WriteLine(actual.AsBinary());

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(2, 6, 0b0001111100)]
        [TestCase(2, 9, 0b1111111100)]
        [TestCase(0, 6, 0b0001111111)]
        public void T01Mask(int i, int j, int expected)
        {
            int actual = Chapter05.P01Mask(i, j);

            Console.WriteLine(actual.AsBinary());

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}