namespace LibProject;

public static class Extensions
{
    public static void Do<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var item in source)
            action(item);
    }
}