using NUnit.Framework;

namespace LibProject.Tests;

public class FizzBuzzerExampleBasedTests
{
    [TestCase(3)]
    [TestCase(6)]
    [TestCase(9)]
    public void Should_Get_Fizz1(int number)
    {
        var result = FizzBuzzer.GetFizzBuzz(number);

        Assert.AreEqual("Fizz", result);
    }

    [TestCaseSource(nameof(FizzTestCases))]
    public void Should_Get_Fizz2(int number)
    {
        var result = FizzBuzzer.GetFizzBuzz(number);

        Assert.AreEqual("Fizz", result);
    }

    private static IEnumerable<int> FizzTestCases()
    {
        const int max = 100;

        for (var i = 1; i <= max; i++)
            if (i % 3 == 0 && i % 5 != 0)
                yield return i;
    }
}