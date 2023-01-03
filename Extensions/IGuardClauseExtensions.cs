using Ardalis.GuardClauses;
using System.Text;

namespace Extensions.GuardClauses;


public static class IGuardClauseExtensions
{
    public static void NullOrWhiteSpace(this IGuardClause self, Rune[] text)
    {
        var str = text.ToString();

        Guard.Against.NullOrWhiteSpace(str);
    }

    public static void EnumValues<T>(this IGuardClause self, T[] allowedValues, T value) where T : Enum
    {
        if (!allowedValues.Contains(value))
            throw new ArgumentException();
    }
}
