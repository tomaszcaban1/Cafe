using Cafe.Domain.Aggregates.MenuAggregate.ValueObjects;
using Cafe.Domain.Common.Models;
using Cafe.Domain.Common.ValueObjects;

namespace Cafe.Domain.Aggregates.MenuAggregate.Entities;

public sealed class MenuItem : Entity<MenuItemId>
{
    MenuItem(MenuItemId id, string name, string description, Price price) : base(id)
    {
        Name = name;
        Description = description;
        ItemPrice = price;
    }

    MenuItem()
    {
    }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public Price ItemPrice { get; private set; }

    public static MenuItem Create(string name, string description, Price price) =>
        new(MenuItemId.CreateUnique,
            name,
            description,
            price);
}
