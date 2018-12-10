using System;

namespace Problems
{
    public static class Chapter06
    {
        private static Random rng = new Random();

        /// <summary>
        /// Math sucks; let's just run an actual simulation.
        /// </summary>
        /// <returns></returns>
        public static double P07TheActualApocalypse(int maxInitialBabies)
        {
            const int simulatedFamilies = 1000000;
            int girls = 0, boys = 0;
            for (int _ = 0; _ < simulatedFamilies; _++)
            {
                var result = P07SimulateAFamily(maxInitialBabies);
                girls += result.Item1;
                boys += result.Item2;
            }

            return Convert.ToDouble(girls) / Convert.ToDouble(girls + boys);
        }

        /// <summary>
        /// Apocalypse where every couple must have a girl. Yep. Returns the ratio of girls.
        /// </summary>
        public static double P07TheApocalypse()
        {
            //! Probability = (1/2) ^ (children)
            //! Scenario    Probability     No. of Boys
            //! ------------------------------------------
            //! G           1/2             1
            //! B G         1/4             2
            //! B B G       1/8             3
            //! B B B G     1/16            4
            //!
            //! Average number of boys:
            //! (1/2) * 1 + (1/4) * 2 + (1/8) * 3 + ...
            //! Average number of girls:
            //! 1, since every family has 1.

            var runningTotal = 0.0;
            var noOfBoys = 0.0;
            var probabilityForNoOfBoys = 1.0 / 2.0;

            while (probabilityForNoOfBoys > 1.0E-12)
            {
                var currentTerm = noOfBoys * probabilityForNoOfBoys;
                runningTotal += currentTerm;

                noOfBoys++;
                probabilityForNoOfBoys /= 2.0;
            }

            //! Since we care about the ratio of girls,
            //! ratio = girls / (girls + boys)
            //! ratio = 1.0 / (1.0 + boys)
            return 1.0 / (1.0 + runningTotal);
        }

        /// <summary>
        /// Flip a coin.
        /// </summary>
        private static bool FlipCoin() => rng.NextDouble() >= 0.5;

        /// <summary>
        /// Simulates a family. Guaranteed 1 girl.
        /// </summary>
        private static Tuple<int, int> P07SimulateAFamily(int maxInitialBabies)
        {
            int girls = 0, boys = 0;

            int initialBabies = rng.Next(0, maxInitialBabies);

            for (int _ = 0; _ < initialBabies; _++)
            {
                if (FlipCoin()) girls++;
                else boys++;
            }

            while (girls == 0)
            {
                if (FlipCoin()) girls++;
                else boys++;
            }

            return Tuple.Create(girls, boys);
        }
    }
}