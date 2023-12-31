using Cafe.Domain.Aggregates.MenuAggregate.ValueObjects;
using Cafe.Domain.Common.Models;

namespace Cafe.Domain.Aggregates.MenuAggregate.Entities;

public sealed class MenuSection : Entity<MenuSectionId>
{
    readonly List<MenuItem> _items = new();

    MenuSection(MenuSectionId id, string name, string description, List<MenuItem> items) : base(id)
    {
        Name = name;
        Description = description;
        _items = items;
    }

    MenuSection()
    {
    }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public IReadOnlyCollection<MenuItem> Items => _items.AsReadOnly();

    public static MenuSection Create(string name,
                                     string description,
                                     List<MenuItem>? items = null) =>
        new(MenuSectionId.CreateUnique,
            name,
            description,
            items ?? new());
}
