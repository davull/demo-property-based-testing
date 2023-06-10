using FsCheck;
using FsCheck.NUnit;

namespace LibProject.Tests;

public class FizzBuzzerPropertyBasedTests
{
    [Property]
    public Property Should_Get_Fizz(PositiveInt n)
    {
        var property = () => FizzBuzzer.GetFizzBuzz(n.Item).Equals("Fizz");

        return property
            .When(n.Item % 3 == 0 && n.Item % 5 != 0)
            .Classify(n.Item > 100, "n > 100")
            .Classify(n.Item < 100, "n < 100");
    }

    [Property]
    public Property Should_Get_Buzz(PositiveInt n)
    {
        var property = () => FizzBuzzer.GetFizzBuzz(n.Item).Equals("Buzz");

        return property
            .When(n.Item % 3 != 0 && n.Item % 5 == 0)
            .Collect($"value is {n}");
    }

    [Property(MaxTest = 10)]
    public Property Should_Get_FizzBuzz(PositiveInt n)
    {
        var property = () => FizzBuzzer.GetFizzBuzz(n.Item).Equals("FizzBuzz");

        return property
            .When(n.Item % 3 == 0 && n.Item % 5 == 0)
            .Collect($"value is {n}");
    }

    [Property]
    public Property Should_Get_Number(PositiveInt n)
    {
        var property = () => FizzBuzzer.GetFizzBuzz(n.Item).Equals(n.Item.ToString());

        return property
            .When(n.Item % 3 != 0 && n.Item % 5 != 0)
            .Collect($"value is {n}");
    }

    [Property(Arbitrary = new[] { typeof(FizzGenerator) })]
    public Property CustomGenerator_Should_Get_Fizz(int n)
    {
        var property = () => FizzBuzzer.GetFizzBuzz(n).Equals("Fizz");

        return property
            .Collect($"value n is {n}");
    }

    private static class FizzGenerator
    {
        // ReSharper disable once UnusedMember.Local
        public static Arbitrary<int> GenerateFizzValues()
        {
            return Arb.From(Gen
                .Choose(1, 1000)
                .Where(n => n % 3 == 0 && n % 5 != 0));
        }
    }
}