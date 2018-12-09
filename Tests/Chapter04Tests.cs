using NUnit.Framework;
using Problems;
using IntNode = Problems.BinaryTreeNode<int>;
using System;

[TestFixture]
public class Chapter04Tests
{
    IntNode exTreeHead;

    /// <summary>
    /// Create example tree to test stuff.
    /// </summary>
    [SetUp]
    public void CreateExampleTree()
    {
        exTreeHead = new IntNode(8);
        exTreeHead.LVal = 4;
        exTreeHead.L.LVal = 2;
        exTreeHead.L.RVal = 6;
        exTreeHead.RVal = 10;
        exTreeHead.R.RVal = 20;
    }

    [Test]
    public void BinaryTreeTest()
    {
        var leftmost = exTreeHead;
        while (!(leftmost.L is null))
            leftmost = leftmost.L;

        var rightmost = exTreeHead;
        while (!(rightmost.R is null))
            rightmost = rightmost.R;

        Console.WriteLine("Tree structure:");
        Console.WriteLine(exTreeHead);

        Assert.That(leftmost.Value, Is.EqualTo(2));
        Assert.That(rightmost.Value, Is.EqualTo(20));
    }
}