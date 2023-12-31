namespace Cafe.Application.Common.Extensions;

public static class StringExtension
{
    public static bool IsValidGuid(this string @this) => Guid.TryParse(@this, out _);

    public static Guid ParseToGuid(this string @this) => Guid.Parse(@this);
}
