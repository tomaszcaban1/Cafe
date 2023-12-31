using Cafe.Domain.Aggregates.MenuReviewAggregate;

namespace Cafe.Application.Common.Interfaces.Persistence;

public interface IMenuReviewRepository
{
    Task AddAsync(MenuReview menuReview, CancellationToken ct);
}
