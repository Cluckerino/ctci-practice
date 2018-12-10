using NUnit.Framework;
using Problems;

namespace Tests
{
    [TestFixture]
    public class Chapter08Tests
    {
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 4)]
        [TestCase(4, 7)]
        [TestCase(5, 13)]
        [TestCase(6, 24)]
        public void T01TripleStep(int input, int expected)
        {
            //! Testing cases:
            //! Steps   Possibilities
            //! 1       1
            //! 2       11, 2
            //! 3       111, 12, 21, 3
            //! 4       1111, 112, 121, 13, 211, 22, 31
            //! 5       11111, 1112, 1121. 113, 1211, 122, 131, 2111, 212, 221, 23, 311, 32

            Assert.Multiple(() =>
            {
                Assert.That(Chapter08.P01TripleStep(input), Is.EqualTo(expected));
                Assert.That(Chapter08.P01Recursive(input), Is.EqualTo(expected));
            });
        }
    }
}