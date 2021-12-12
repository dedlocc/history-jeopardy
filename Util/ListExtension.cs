namespace HistoryJeopardy.Util;

public static class ListExtension
{
    private static readonly Random Random = new();

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        return source.OrderBy(_ => Random.Next());
    }
}