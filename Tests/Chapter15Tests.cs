﻿using NUnit.Framework;
using Problems;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class Chapter15Tests
    {
        [Test]
        public void T03DiningPhilosophers()
        {
            const int num = 5;
            var table = new Chapter15.P03Table(num);

            // Should be all true if no one starves.
            var results = table.Dudes
                .AsParallel()
                .Select(d => d.TryToEat())
                .AsSequential()
                .ToList();
            Assert.That(results.Count, Is.EqualTo(table.Dudes.Count));
            Assert.That(results, Is.All.True);
        }
    }
}