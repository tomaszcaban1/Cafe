using Cafe.Domain.Common.Models;

namespace Cafe.Domain.Aggregates.UserAggregate.ValueObjects;

public sealed class UserId : AggregateRootId<Guid>
{
    UserId(Guid value)
    {
        Value = value;
    }

    UserId()
    {
    }

    public override Guid Value { get; protected set; }

    public static UserId CreateUnique() => new(Guid.NewGuid());

    public static UserId Create(Guid value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
