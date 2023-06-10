namespace LibProject;

public static class FizzBuzzer
{
    public static string GetFizzBuzz(int number)
    {
        var tuple = (IsFizz(number), IsBuzz(number));
        var result = GetString(tuple, number);
        return result;
    }

    private static string GetString((bool, bool) tuple, int number)
    {
        return tuple switch
        {
            (true, true) => "FizzBuzz",
            (true, false) => "Fizz",
            (false, true) => "Buzz",
            _ => number.ToString()
        };
    }

    private static bool IsFizz(int n)
    {
        return n % 3 == 0;
    }

    private static bool IsBuzz(int n)
    {
        return n % 5 == 0;
    }
}