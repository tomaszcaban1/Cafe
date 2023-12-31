using Cafe.Domain.Common.Models.Interfaces;

namespace Cafe.Domain.Aggregates.MenuAggregate.Events;

public record MenuCreated(Menu Menu) : IDomainEvent;
