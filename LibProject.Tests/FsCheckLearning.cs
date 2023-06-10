using FsCheck;
using NUnit.Framework;

namespace LibProject.Tests;

public class FsCheckLearning
{
    [Test]
    public void Generate_Integers()
    {
        var generator = Gen.Choose(0, 100)
            .Where(i => i % 2 == 0);

        var sample = generator
            .Sample(0, 10);

        WriteList(sample);
    }

    [Test]
    public void Generate_Booleans()
    {
        var generator = Gen.OneOf(
            Gen.Constant(true),
            Gen.Constant(false));

        var sample = generator
            .Select(b => b ? "ja" : "nein")
            .Sample(0, 10);

        WriteList(sample);
    }

    [Test]
    public void Generate_Primitive_Data()
    {
        var arbitrary = Arb.Default.PositiveInt();

        const int max = 20;
        const int count = 10;

        var sample = Gen
            .Sample(max, count, arbitrary.Generator)
            .Select(i => i.Item);

        WriteList(sample);
    }

    [Test]
    public void Generate_Complex_Data()
    {
        var generator = Arb.Generate<Point>();

        var sample = generator
            .Sample(100, 10);

        WriteList(sample);
    }

    [Test]
    public void Generate_Custom_Complex_Data()
    {
        var positiveNumberGenerator = Arb.Default.PositiveInt().Generator;
        var negativeNumberGenerator = Arb.Default.NegativeInt().Generator;

        var generator1 = positiveNumberGenerator
            .Select(i => i.Item)
            .Select(i => new Point(i, i));

        var generator2 = from x in positiveNumberGenerator
                         from y in negativeNumberGenerator
                         select new Point(x.Item, y.Item);

        var sample1 = generator1
            .Sample(100, 10);

        var sample2 = generator2
            .Sample(100, 10);

        WriteList(sample1);
        WriteList(sample2);
    }

    [Test]
    public void Test_Property1()
    {
        // Step 1: Create a arbitrary generator
        var arbitrary = Arb.From<int>()
            .Filter(i => i % 2 == 0);

        // Step 2: Define property
        var property = (int i) => i % 2 == 0;

        // Step 3: Test
        var test = Prop
            .ForAll(arbitrary, property)
            .Label("Value is even");

        test.QuickCheckThrowOnFailure();
    }

    [FsCheck.NUnit.Property]
    public Property Test_Property2(PositiveInt value)
    {
        var property = () => value.Item % 2 == 0;

        return property
            .When(value.Item % 2 == 0)
            .Classify(value.Get > 10, "Value is greater than 10")
            .Classify(value.Get < 10, "Value is smaller than 10");
    }

    [FsCheck.NUnit.Property]
    public bool Test_Property3(PositiveInt value)
    {
        return value.Item > 0;
    }

    private static void WriteList<T>(IEnumerable<T> list)
    {
        list
            .Select((s, i) => $"[{i}]: {s}")
            .Do(TestContext.WriteLine);

        TestContext.WriteLine("-------------------------");
    }

    private record Point(int X, int Y);
}