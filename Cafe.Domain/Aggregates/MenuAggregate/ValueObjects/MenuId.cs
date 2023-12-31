using Cafe.Domain.Common.Models;

namespace Cafe.Domain.Aggregates.MenuAggregate.ValueObjects;

public sealed class MenuId : AggregateRootId<Guid>
{
    MenuId(Guid value)
    {
        Value = value;
    }

    MenuId()
    {
    }

    public override Guid Value { get; protected set; }

    public static MenuId CreateUnique() => new(Guid.NewGuid());

    public static MenuId Create(Guid value) => new MenuId(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
