namespace Notix.Beta.API.Shared.Extensions.String;

public static class StringExtensions
{
    public static string ToSnakeCase(this string? str)
    {
        static IEnumerable<char> Convert(IEnumerator<char> charEnumerator)
        {
            if(!charEnumerator.MoveNext())
                yield break;
            yield return char.ToLower(charEnumerator.Current);
            while (charEnumerator.MoveNext())
            {
                if (char.IsUpper(charEnumerator.Current))
                    yield return '_';
                yield return char.ToLower(charEnumerator.Current);
            }
        }
        return new string(Convert(str!.GetEnumerator()).ToArray());
    }
}