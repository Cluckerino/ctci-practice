using NUnit.Framework;
using Problems;

namespace Tests
{
    [TestFixture]
    public class Chapter01Tests
    {
        [TestCase("qwertyuiopasdfghjklzxcvbnm", true)]
        [TestCase("qwertyuiopasdfghjklzxcvbnmq", false)]
        public void T01IsUnique(string input, bool expected)
        {
            Assert.AreEqual(Chapter01.P01IsUnique(input), expected);
        }

        [TestCase("pale", "ple", true)]
        [TestCase("pales", "pale", true)]
        [TestCase("pale", "bale", true)]
        [TestCase("pale", "bake", false)]
        public void T05OneAway(string i1, string i2, bool expected)
        {
            Assert.AreEqual(Chapter01.P05OneAway(i1, i2), expected);
        }
    }
}