using Cafe.Domain.Aggregates.HostAggregate.ValueObjects;
using Cafe.Domain.Aggregates.MenuAggregate.ValueObjects;
using Cafe.Domain.Aggregates.UserAggregate.ValueObjects;
using Cafe.Domain.Common.Models;

namespace Cafe.Domain.Aggregates.HostAggregate;

public sealed class Host : AggregateRoot<HostId, string>
{
    readonly List<MenuId> _menuIds = new();

    Host(HostId hostId, string name, UserId userId) : base(hostId)
    {
        Name = name;
        UserId = userId;
    }

    Host()
    {
    }

    public string Name { get; private set; } = default!;

    public UserId UserId { get; private set; }

    public IReadOnlyList<MenuId> MenuIds => _menuIds.AsReadOnly();

    public DateTime CreatedDateTime { get; private set; }

    public DateTime UpdatedDateTime { get; private set; }

    public static Host Create(UserId userId, string name) =>
        new Host(
            HostId.Create(userId),
            name,
            userId
        );

    public void AddMenuId(MenuId menuId)
    {
        _menuIds.Add(menuId);
    }
}