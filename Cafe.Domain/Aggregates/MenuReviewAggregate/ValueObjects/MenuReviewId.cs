using Cafe.Domain.Common.Models;

namespace Cafe.Domain.Aggregates.MenuReviewAggregate.ValueObjects;

public sealed class MenuReviewId : AggregateRootId<Guid>
{
    MenuReviewId(Guid value)
    {
        Value = value;
    }

    MenuReviewId()
    {
    }

    public override Guid Value { get; protected set; }

    public static MenuReviewId CreateUnique() => new(Guid.NewGuid());

    public static MenuReviewId Create(Guid value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
