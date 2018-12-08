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
    }
}