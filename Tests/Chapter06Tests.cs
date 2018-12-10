using NUnit.Framework;
using Problems;

namespace Tests
{
    [TestFixture]
    public class Chapter06Tests
    {
        [TestCase(0)]
        [TestCase(2)]
        [TestCase(10)]
        public void T07TheActualApocalypse(int maxInitialBabies)
        {
            Assert.Pass($"Total fraction of girls: {Chapter06.P07TheActualApocalypse(maxInitialBabies)}");
        }

        [Test]
        public void T07TheApocalypse()
        {
            Assert.Pass($"Total fraction of girls: {Chapter06.P07TheApocalypse()}");
        }
    }
}