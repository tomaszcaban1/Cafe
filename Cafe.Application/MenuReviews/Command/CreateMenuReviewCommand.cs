using Cafe.Application.Common.Interfaces.Behaviors;
using Cafe.Domain.Aggregates.MenuReviewAggregate;
using ErrorOr;

namespace Cafe.Application.MenuReviews.Command;

public record CreateMenuReviewCommand(
    int RatingValue,
    string Comment,
    string UserId,
    string MenuId    
) : IValidatableRequest<ErrorOr<MenuReview>>;
