using Cafe.Domain.Common.Models.Interfaces;

namespace Cafe.Domain.Aggregates.MenuReviewAggregate.Events;

public record MenuReviewCreated(MenuReview MenuReview) : IDomainEvent;
