namespace Cafe.Contracts.MenuReview;

public record MenuReviewResponse(string Id, int RatingValue, string Comment, string UserId, string MenuId);
