using Cafe.Domain.Common.Models;

namespace Cafe.Domain.Aggregates.MenuAggregate.ValueObjects;

public sealed class MenuSectionId : ValueObject
{
    MenuSectionId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; private set; }

    public static MenuSectionId CreateUnique => new(Guid.NewGuid());

    public static MenuSectionId Create(Guid value) => new MenuSectionId(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
