using System.Text;

namespace Extensions.String;


public static class StringExtensions
{
    public static Rune[] ToRuneArray(this string self)
    {
        var array = self
            .EnumerateRunes()
            .ToArray();

        return array;
    }
}
