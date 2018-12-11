namespace Problems
{
    public static class Chapter08
    {
        /// <summary>
        /// Given a staircase with N steps, count how many ways you can up it if you can go 1, 2, or
        /// 3 steps at a time. Iterative solution here.
        /// </summary>
        public static int P01TripleStepIter(int numSteps)
        {
            // Sum of ways = ways after taking 1 step + ways after taking 2 steps + ways after taking
            // 3 steps. Essentially, S(N) = S(N-1) + S(N-2) + S(N-3), and the problem is a tribonnaci
            // sequence.

            // To initalize the sequence, define: S(0) = 1; S(N) = 0 if N < 0
            var n1 = 1;
            var n2 = 0;
            var n3 = 0;
            var currentSum = 0;

            for (var i = 1; i <= numSteps; i++)
            {
                currentSum = n1 + n2 + n3;
                n3 = n2;
                n2 = n1;
                n1 = currentSum;
            }

            return currentSum;
        }

        /// <summary>
        /// Given a staircase with N steps, count how many ways you can up it if you can go 1, 2, or
        /// 3 steps at a time. Slow, bulky recursive solution here.
        /// </summary>
        public static int P01TripleStepRec(int numStepsLeft)
        {
            // Base cases, take the last 1, 2, or 3 steps, or recurse.
            if (numStepsLeft == 1)
                return 1;
            else if (numStepsLeft == 2)
                return 1 + P01TripleStepRec(numStepsLeft - 1);
            else if (numStepsLeft == 3)
                return 1 + P01TripleStepRec(numStepsLeft - 1) + P01TripleStepRec(numStepsLeft - 2);

            // Everything else, imagine youre taking 1, 2, or 3 steps first. Count possibilities.
            return P01TripleStepRec(numStepsLeft - 1) +
                P01TripleStepRec(numStepsLeft - 2) +
                P01TripleStepRec(numStepsLeft - 3);
        }
    }
}