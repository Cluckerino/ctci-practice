﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Problems
{
    public static class Chapter15
    {
        /// <summary>
        /// Chopsticks.
        /// </summary>
        public class P03Chopstick
        {
            private int id;

            /// <summary>
            /// Make a chopstick.
            /// </summary>
            public P03Chopstick(int id)
            {
                this.id = id;
            }

            /// <summary>
            /// Identifies this thing.
            /// </summary>
            public override string ToString() => $"C{id}";
        }

        /// <summary>
        /// A dining philosopher.
        /// </summary>
        public class P03Dude
        {
            /// <summary>
            /// Waiting interval.
            /// </summary>
            public const int TimeToWait = 50;

            private readonly int id;

            private readonly P03Table table;

            /// <summary>
            /// Make a dude/dudette.
            /// </summary>
            public P03Dude(int id, P03Table table)
            {
                this.id = id;
                this.table = table;
            }

            /// <summary>
            /// Identifies this thing.
            /// </summary>
            public override string ToString() => $"P{id}";

            /// <summary>
            /// Try to eat. If you wait too long before finishing, you will die (i.e. return false).
            /// </summary>
            public bool TryToEat()
            {
                //! 3 * time = since we wait 2 x times when eating + buffer.
                //! Multiply by number of dudes to give them a chance to eat.
                var lifespan = 4 * TimeToWait * table.Dudes.Count;

                var a = new Thread(() => Eat());
                a.Start();
                // If thread terminates, there wasn't any deadlock. Return true.
                if (a.Join(lifespan))
                    return true;

                // If we hit here, the thread timed out.
                Console.WriteLine($"{this} died of starvation.");
                return false;
            }

            /// <summary>
            /// Eat.
            /// </summary>
            private void Eat()
            {
                Console.WriteLine($"{this} started eating.");
                var leftStick = table.GetChopstick(id);
                var rightStick = table.GetChopstick(id + 1);

                Console.WriteLine($"{this} wants left chopstick.");
                while (!Monitor.TryEnter(leftStick))
                    Thread.Sleep(TimeToWait);
                Console.WriteLine($"{this} got left chopstick {leftStick}!");

                Console.WriteLine($"{this} wants right chopstick.");
                while (!Monitor.TryEnter(rightStick))
                    Thread.Sleep(TimeToWait);
                Console.WriteLine($"{this} got right chopstick {leftStick}!");

                Console.WriteLine($"{this} is eating.");
                Thread.Sleep(TimeToWait);

                Monitor.Exit(leftStick);
                Monitor.Exit(rightStick);
                Console.WriteLine($"{this} finished eating.");
            }
        }

        /// <summary>
        /// The dining table for philosophers.
        /// </summary>
        public class P03Table
        {
            /// <summary>
            /// Create a table with a given number of philosphers and chopsticks.
            /// </summary>
            public P03Table(int num)
            {
                var nums = Enumerable.Range(0, num);
                Sticks = nums.Select(i => new P03Chopstick(i)).ToList();
                Dudes = nums.Select(i => new P03Dude(i, this)).ToList();
            }

            /// <summary>
            /// Philosophers tryna get fed.
            /// </summary>
            public List<P03Dude> Dudes { get; }

            /// <summary>
            /// Chopsticks in the table.
            /// </summary>
            public List<P03Chopstick> Sticks { get; }

            /// <summary>
            /// Get a chopstick using the given index. Same num is left, +1 (ring array) is right.
            /// </summary>
            public P03Chopstick GetChopstick(int num) =>
                Sticks[(Sticks.Count + num) % Sticks.Count];

            /// <summary>
            /// Get a dining philosopher.
            /// </summary>
            public P03Dude GetDude(int num) => Dudes[num];
        }
    }
}