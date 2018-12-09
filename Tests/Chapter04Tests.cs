using NUnit.Framework;
using Problems;
using IntNode = Problems.BinaryTreeNode<int>;
using System;

[TestFixture]
public class Chapter04Tests
{
    [Test]
    public void BinaryTreeTest()
    {
        var h = new IntNode(8);
        h.LVal = 4;
        h.L.LVal = 2;
        h.L.RVal = 6;
        h.RVal = 10;
        h.R.RVal = 20;

        var leftmost = h;
        while (!(leftmost.L is null))
            leftmost = leftmost.L;

        var rightmost = h;
        while (!(rightmost.R is null))
            rightmost = rightmost.R;

        Console.WriteLine("Tree structure:");
        Console.WriteLine(h);

        Assert.That(leftmost.Value, Is.EqualTo(2));
        Assert.That(rightmost.Value, Is.EqualTo(20));
    }
}