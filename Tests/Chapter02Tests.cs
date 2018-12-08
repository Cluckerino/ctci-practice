using NUnit.Framework;
using Problems;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class Chapter02Tests
    {
        public void P01_RemoveDupes(IEnumerable<int> input, IEnumerable<int> expected)
        {
            var inputList = new LinkedList<int>(input);
            var expectedList = new LinkedList<int>(expected);
            Assert.That(Chapter02.P01_RemoveDupes(inputList), Is.EqualTo(expectedList));
        }

        [Test]
        public void P01_RemoveDupes_HasDupes() =>
            P01_RemoveDupes(new[] { 3, 4, 5, 6, 3, 3, 5, 6, 7 }, new[] { 3, 4, 5, 6, 7 });

        [Test]
        public void P01_RemoveDupes_NoDupes() =>
            P01_RemoveDupes(new[] { 3, 4, 5, 6, 7 }, new[] { 3, 4, 5, 6, 7 });
    }
}