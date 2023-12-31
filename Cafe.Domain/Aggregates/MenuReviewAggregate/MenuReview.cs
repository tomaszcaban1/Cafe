using Cafe.Domain.Aggregates.HostAggregate.ValueObjects;
using Cafe.Domain.Aggregates.MenuAggregate.ValueObjects;
using Cafe.Domain.Aggregates.MenuReviewAggregate.Events;
using Cafe.Domain.Aggregates.MenuReviewAggregate.ValueObjects;
using Cafe.Domain.Aggregates.UserAggregate.ValueObjects;
using Cafe.Domain.Common.Models;
using Cafe.Domain.Common.ValueObjects;

namespace Cafe.Domain.Aggregates.MenuReviewAggregate;

public sealed class MenuReview : AggregateRoot<MenuReviewId, Guid>
{
    MenuReview(
        MenuReviewId menuReviewId,
        Rating rating,
        string comment,
        HostId hostId,
        UserId userId,
        MenuId menuId
        )
        : base(menuReviewId)
    {
        Rating = rating;
        Comment = comment;
        HostId = hostId;
        UserId = userId;
        MenuId = menuId; 
    }

    MenuReview()
    {
    }

    public Rating Rating { get; private set; }

    public string Comment { get; private set; }

    public HostId HostId { get; private set; }

    public UserId UserId { get; private set; }

    public MenuId MenuId { get; private set; }

    public static MenuReview Create(
        int rating,
        string comment,
        HostId hostId,
        UserId userId,
        MenuId menuId,
        MenuReviewId? menuReviewId = null)
    {
        var ratingValueObject = Rating.Create(rating);

        var menuReview = new MenuReview(
            menuReviewId ?? MenuReviewId.CreateUnique(),
            ratingValueObject,
            comment,
            hostId,
            userId,
            menuId
            );

        menuReview.AddDomainEvent(new MenuReviewCreated(menuReview));

        return menuReview;
    }
}
