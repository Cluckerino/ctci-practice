using NUnit.Framework;
using Problems;

namespace Tests
{
    public class Chapter03Tests
    {
        [Test]
        public void T01ThreeInOne()
        {
            var stacky = new Chapter03.P01ThreeInOne<int>();

            const int nLength = 5;
            var expectedPush1 = new [] { 1, 2, 3, 4, 5 };
            var expectedPush2 = new [] { 6, 7, 8, 9, 10 };
            var expectedPush3 = new [] { 11, 12, 13, 14, 15 };

            for (int n1 = 1; n1 <= nLength; n1++)
            {
                int n2 = n1 + nLength;
                int n3 = n1 + nLength + nLength;

                stacky.Push1(n1);
                stacky.Push2(n2);
                stacky.Push3(n3);
            }

            var actualPush1 = stacky.List1();
            var actualPush2 = stacky.List2();
            var actualPush3 = stacky.List3();

            Assert.Multiple(() =>
            {
                Assert.That(actualPush1, Is.EqualTo(expectedPush1));
                Assert.That(actualPush2, Is.EqualTo(expectedPush2));
                Assert.That(actualPush3, Is.EqualTo(expectedPush3));
            });

            for (int n1 = 0; n1 < 2; n1++)
            {
                stacky.Pop1();
                stacky.Pop2();
                stacky.Pop3();
            }

            var expectedPop1 = new [] { 1, 2, 3 };
            var expectedPop2 = new [] { 6, 7, 8 };
            var expectedPop3 = new [] { 11, 12, 13 };

            var actualPop1 = stacky.List1();
            var actualPop2 = stacky.List2();
            var actualPop3 = stacky.List3();

            Assert.Multiple(() =>
            {
                Assert.That(actualPop1, Is.EqualTo(expectedPop1));
                Assert.That(actualPop2, Is.EqualTo(expectedPop2));
                Assert.That(actualPop3, Is.EqualTo(expectedPop3));
            });
        }

        [Test]
        public void T04QueueViaStacks()
        {
            int input1 = 1, input2 = 2;
            int expected1 = 1, expected2 = 2;
            var queue = new Chapter03.P04QueueViaStacks<int>();

            queue.Enqueue(input1);
            queue.Enqueue(input2);

            Assert.Multiple(() =>
            {
                Assert.That(queue.Dequeue(), Is.EqualTo(expected1));
                Assert.That(queue.Dequeue(), Is.EqualTo(expected2));
            });
        }
    }
}