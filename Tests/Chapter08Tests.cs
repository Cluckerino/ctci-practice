using System.Collections.Generic;
using NUnit.Framework;
using Problems;

namespace Tests
{
    [TestFixture]
    public class Chapter08Tests
    {
        public static IEnumerable<TestCaseData> T01TripleStepCases
        {
            get
            {
                var useIterativeOptions = new [] { true, false };

                foreach (var useIterative in useIterativeOptions)
                {
                    yield return new TestCaseData(1, useIterative, 1);
                    yield return new TestCaseData(2, useIterative, 2);
                    yield return new TestCaseData(3, useIterative, 4);
                    yield return new TestCaseData(4, useIterative, 7);
                    yield return new TestCaseData(5, useIterative, 13);
                    yield return new TestCaseData(26, useIterative, 4700770);
                    yield return new TestCaseData(27, useIterative, 8646064);
                }
            }
        }

        [TestCaseSource(typeof(Chapter08Tests), nameof(T01TripleStepCases))]
        public void T01TripleStep(int input, bool useIterative, int expected)
        {
            //! Testing cases:
            //! Steps   Possibilities
            //! 1       1
            //! 2       11, 2
            //! 3       111, 12, 21, 3
            //! 4       1111, 112, 121, 13, 211, 22, 31
            //! 5       11111, 1112, 1121. 113, 1211, 122, 131, 2111, 212, 221, 23, 311, 32

            int actual = useIterative ?
                Chapter08.P01TripleStepIter(input) :
                Chapter08.P01TripleStepRec(input);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}