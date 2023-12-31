﻿using Cafe.Domain.Aggregates.HostAggregate.ValueObjects;
using Cafe.Domain.Aggregates.MenuAggregate.Entities;
using Cafe.Domain.Aggregates.MenuAggregate.Events;
using Cafe.Domain.Aggregates.MenuAggregate.ValueObjects;
using Cafe.Domain.Aggregates.MenuReviewAggregate.ValueObjects;
using Cafe.Domain.Common.Models;

namespace Cafe.Domain.Aggregates.MenuAggregate;

public sealed class Menu : AggregateRoot<MenuId, Guid>
{
    readonly List<MenuSection> _sections = new();

    readonly List<MenuReviewId> _menuReviewIds = new();

    Menu(
        MenuId menuId,
        HostId hostId,
        string name,
        string description,
        List<MenuSection> sections)
    : base(menuId)
    {
        HostId = hostId;
        Name = name;
        Description = description;
        _sections = sections;
    }

    Menu()
    {
    }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public IReadOnlyList<MenuSection> Sections => _sections.AsReadOnly();

    public HostId HostId { get; private set; }

    public IReadOnlyList<MenuReviewId> MenuReviewIds => _menuReviewIds.AsReadOnly();

    public DateTime CreatedDateTime { get; private set; }

    public DateTime UpdatedDateTime { get; private set; }

    public static Menu Create(
        HostId hostId,
        string name,
        string description,
        List<MenuSection>? sections = null)
    {
        var menu = new Menu(
            MenuId.CreateUnique(),
            hostId,
            name,
            description,
            sections ?? new()
        );

        menu.AddDomainEvent(new MenuCreated(menu));

        return menu;
    }

    public void AddMenuReviewId(MenuReviewId menuReviewId) => _menuReviewIds.Add(menuReviewId);
}
