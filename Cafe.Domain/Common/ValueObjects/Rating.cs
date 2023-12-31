using Cafe.Domain.Common.Models;

namespace Cafe.Domain.Common.ValueObjects;

public sealed class Rating : ValueObject
{
    public Rating(int value)
    {
        Value = value;
    }

    public int Value { get; private set; }

    public static Rating Create(int value) => new Rating(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
