using System.Text.RegularExpressions;

namespace Fyter.WebApp.Utilities;

public static class StringHelper
{
    public static HashSet<string> excludedWords = ["FighterInfo", "Fighter"];

    /// <summary>
    /// Returns:
    ///   - If no periods, the entire string (pascal-humanized).
    ///   - Otherwise, the last two segments (or only the last one if the second-last is excluded).
    ///   - Each segment is converted from PascalCase to spaced words.
    /// </summary>
    /// <param name="input">The input string, possibly containing periods.</param>
    /// <returns>The humanized result.</returns>
    public static string GetToolTipPropertyTitle(string input)
    {
        // Split on periods
        string[] parts = input.Split('.');

        // If there's only one part (no periods), just return it humanized.
        if (parts.Length == 1)
        {
            return ConvertPascalToWords(parts[0]);
        }

        // Otherwise, we want the last two segments
        // (Regardless of how many dots, we only care about the last two).
        string secondLast = parts[parts.Length - 2];
        string last = parts[parts.Length - 1];

        // If the second-last segment is in the excluded list, skip it and use only the last
        if (excludedWords.Contains(secondLast))
        {
            return ConvertPascalToWords(last);
        }
        else
        {
            // Use both segments, but humanize each before joining
            return $"{ConvertPascalToWords(secondLast)} {ConvertPascalToWords(last)}";
        }
    }

    /// <summary>
    /// Converts a PascalCase or camelCase string into spaced words:
    ///   "HomeTown" -> "Home Town"
    ///   "PunchPower" -> "Punch Power"
    ///   "TopMoves" -> "Top Moves"
    /// </summary>
    private static string ConvertPascalToWords(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        // Insert a space before each uppercase letter that follows a lowercase letter
        // e.g. "HeadMovement" -> "Head Movement"
        string spaced = Regex.Replace(input, "(?<=[a-z])([A-Z])", " $1");

        // Also handle any edge cases if desired, e.g., acronyms, numbers, etc.
        if (spaced.Equals("Value"))
            spaced = "Rating";

        return spaced.Trim();
    }
}
