using FsCheck;
using NUnit.Framework;
using Random = System.Random;

namespace LibProject.Tests;

public class MathOperationsTests
{
    [Test]
    public void Add_TestCommutativity_FsCheck()
    {
        var property = (int x, int y)
            => MathOperations.Add(x, y) == MathOperations.Add(y, x);

        Prop.ForAll(
                Arb.From<int>(),
                Arb.From<int>(),
                property)
            .QuickCheckThrowOnFailure();
    }

    [TestCase(0, 0)]
    [TestCase(1, 2)]
    [TestCase(999, 9999)]
    public void Add_TestCommutativity_Simple(int a, int b)
    {
        var left = MathOperations.Add(a, b);
        var right = MathOperations.Add(b, a);

        Assert.AreEqual(left, right);
    }

    [Test]
    public void Add_TestCommutativity_Random()
    {
        for (var i = 0; i < 1_000; i++)
        {
            var a = Random.Shared.Next(-99_999, 99_999);
            var b = Random.Shared.Next(-99_999, 99_999);
            TestContext.WriteLine($"a: {a}, b: {b}");

            var left = MathOperations.Add(a, b);
            var right = MathOperations.Add(b, a);

            Assert.AreEqual(left, right);
        }
    }

    [FsCheck.NUnit.Property]
    public Property Add_TestCommutativity(int x, int y)
    {
        // x + y == y + x
        var property = MathOperations.Add(x, y) == MathOperations.Add(y, x);

        return property
            .Classify(x == 0, "x == 0")
            .Classify(x > 0, "x > 0")
            .Classify(x < 0, "x < 0");
    }

    [FsCheck.NUnit.Property]
    public Property Add_TestCommutativity_WithCollect(int x, int y)
    {
        // x + y == y + x
        var property = MathOperations.Add(x, y) == MathOperations.Add(y, x);

        return property
            .Collect($"x: {x}, y: {y}");
    }

    [FsCheck.NUnit.Property]
    public bool Add_TestAssociativity(int x, int y, int z)
    {
        // x + (y + z) == (x + y) + z
        return MathOperations.Add(x, MathOperations.Add(y, z)) ==
               MathOperations.Add(MathOperations.Add(x, y), z);
    }

    [FsCheck.NUnit.Property]
    public bool Add_TestAdditiveIdentity(int x)
    {
        // x + 0 == x
        return MathOperations.Add(x, 0) == x;
    }

    [FsCheck.NUnit.Property]
    public bool Add_TestAdditiveInverse(int x)
    {
        // x + (-x) == 0
        return MathOperations.Add(x, -x) == 0;
    }

    [FsCheck.NUnit.Property]
    public Property Divide(int x, int y)
    {
        var property = () => MathOperations.Divide(x * y, y) == x;
        return property.When(y != 0);
    }
}