using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Problems
{
    public static class Chapter15
    {
        /// <summary>
        /// The dining table for philosophers.
        /// </summary>
        public class P03Table
        {
            /// <summary>
            /// Chopsticks in the table.
            /// </summary>
            public List<P03Chopstick> Sticks { get; }

            /// <summary>
            /// Philosophers tryna get fed.
            /// </summary>
            public List<P03Dude> Dudes { get; }

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
            /// Get a chopstick. Same num is left, +1 (ring array) is right. Locks if 
            /// </summary>
            public P03Chopstick GetChopstick(int num) =>
                Sticks[(Sticks.Count + num) % Sticks.Count];

            /// <summary>
            /// Get a dining philosopher.
            /// </summary>
            public P03Dude GetDude(int num) => Dudes[num];
        }

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
            /// Waiting intervals.
            /// </summary>
            public const int TimeToWait = 50;

            private int id;

            /// <summary>
            /// Make a dude/dudette.
            /// </summary>
            public P03Dude(int id, P03Table table)
            {
                this.id = id;
                this.table = table;
            }

            P03Table table;
            /// <summary>
            /// Identifies this thing.
            /// </summary>
            public override string ToString() => $"P{id}";

            /// <summary>
            /// Get the left stick.
            /// </summary>
            private P03Chopstick GetLeftStick()
            {
                Console.WriteLine($"{this} wants left chopstick.");
                var stick = table.GetChopstick(id);
                Console.WriteLine($"{this} got {stick}!");
                return stick;
            }

            /// <summary>
            /// Eat.
            /// </summary>
            private void Eat()
            {
                Console.WriteLine($"{this} started eating.");
                lock(GetLeftStick())
                {
                    Thread.Sleep(TimeToWait);

                    lock(GetRightStick())
                    {
                        Console.WriteLine($"{this} is eating.");
                        Thread.Sleep(TimeToWait);
                    }
                }
                Console.WriteLine($"{this} finished eating.");
            }

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
            /// Get the right stick.
            /// </summary>
            private P03Chopstick GetRightStick()
            {
                Console.WriteLine($"{this} wants right chopstick.");
                var stick = table.GetChopstick(id + 1);
                Console.WriteLine($"{this} got {stick}!");
                return stick;
            }
        }
    }
}