using System.Text.RegularExpressions;

namespace RentalRoomCrawler.Helpers;

public static class StringHelper
{
    private static string RemoveSpaces(this string value)
    {
        return Regex.Replace(value, @"\s+", " ");
    }

    public static string RemoveBreakLineTab(this string input)
    {
        if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input)) return "";

        var line = input
            .Replace("\t", string.Empty)
            .Replace("\r", string.Empty)
            .Replace("\n", string.Empty)
            .RemoveSpaces()
            .Trim();

        return line;

    }

    public static string ReplaceMultiToEmpty(this string input, List<string> listForRemove)
    {
        if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input)) return "";

        return listForRemove.Aggregate(input, (current, t) => current.Replace(t, string.Empty));
    }
}