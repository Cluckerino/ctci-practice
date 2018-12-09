using System.Collections.Generic;
using System.Linq;
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
            var inputs = new [] { 1, 2, 3, 4, 5 };

            var queue = new Chapter03.P04QueueViaStacks<int>();

            foreach (var input in inputs)
                queue.Enqueue(input);

            Assert.Multiple(() =>
            {
                foreach (var expected in inputs)
                {
                    Assert.That(queue.IsEmpty(), Is.False);
                    Assert.That(queue.Peek(), Is.EqualTo(expected));
                    Assert.That(queue.Dequeue(), Is.EqualTo(expected));
                }
                Assert.That(queue.IsEmpty(), Is.True);
            });
        }

        [Test]
        public void T05SortStack()
        {
            var inputs = new [] { 0, 18, 9, 24, 5, 5, 27, 13, 11, 1 };
            var expecteds = inputs.OrderBy(i => i).ToArray();
            var stack = new Stack<int>(inputs);

            Chapter03.P05SortStack(stack);

            Assert.That(stack.Count, Is.EqualTo(expecteds.Length));
            Assert.Multiple(() =>
            {
                foreach (var expected in expecteds)
                    Assert.That(stack.Pop(), Is.EqualTo(expected));
            });
        }
    }
}