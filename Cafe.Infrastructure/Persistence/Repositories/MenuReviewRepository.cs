using Cafe.Application.Common.Interfaces.Persistence;
using Cafe.Domain.Aggregates.MenuReviewAggregate;

namespace Cafe.Infrastructure.Persistence.Repositories;

public class MenuReviewRepository : IMenuReviewRepository
{
    readonly CafeDbContext _dbContext;

    public MenuReviewRepository(CafeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task AddAsync(MenuReview menuReview, CancellationToken ct)
    {
        _dbContext.MenuReviews.Add(menuReview);
        return _dbContext.SaveChangesAsync(ct);
    }
}
