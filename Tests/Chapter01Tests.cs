using NUnit.Framework;
using Problems;

namespace Tests
{
    [TestFixture]
    public class Chapter01Tests
    {
        [TestCase("qwertyuiopasdfghjklzxcvbnm", true)]
        [TestCase("qwertyuiopasdfghjklzxcvbnmq", false)]
        public void P01_IsUnique(string input, bool expected)
        {
            Assert.AreEqual(Chapter01.P01_IsUnique(input), expected);
        }

        [TestCase("pale","ple", true)]
        [TestCase("pales","pale", true)]
        [TestCase("pale","bale", true)]
        [TestCase("pale","bake", false)]
        public void P05_OneAway(string i1, string i2, bool expected)
        {
            Assert.AreEqual(Chapter01.P05_OneAway(i1, i2), expected);
        }
    }
}