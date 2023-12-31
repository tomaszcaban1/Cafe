using Cafe.Domain.Common.Models;

namespace Cafe.Domain.Aggregates.MenuAggregate.ValueObjects;

public sealed class MenuItemId : ValueObject
{
    MenuItemId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; private set; }

    public static MenuItemId CreateUnique => new(Guid.NewGuid());

    public static MenuItemId Create(Guid value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
