namespace Problems
{
    public static class Chapter08
    {
        /// <summary>
        /// Given a staircase with N steps, count how many ways you can up it if you can go 1, 2,
        /// or 3 steps at a time.
        /// </summary>
        public static int P01TripleStep(int numSteps)
        {
            return P01Recursive(numSteps);
        }

        /// <summary>
        /// Recursive triple step answer.
        /// </summary>
        public static int P01Recursive(int numStepsLeft)
        {
            // Base cases, take the last 1, 2, or 3 steps, or recurse.
            if (numStepsLeft == 1)
                return 1;
            else if (numStepsLeft == 2)
                return 1 + P01Recursive(numStepsLeft - 1);
            else if (numStepsLeft == 3)
                return 1 + P01Recursive(numStepsLeft - 1) + P01Recursive(numStepsLeft - 2);

            // Everything else, imagine youre taking 1, 2, or 3 steps first. cout possibilities.
            return P01Recursive(numStepsLeft - 1) + P01Recursive(numStepsLeft - 2) + P01Recursive(numStepsLeft - 3);
        }
    }
}