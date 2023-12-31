using Cafe.Domain.Aggregates.UserAggregate;
using Cafe.Domain.Aggregates.UserAggregate.ValueObjects;

namespace Cafe.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    Task<bool> IsEmailTaken(string email, CancellationToken ct);

    Task<User?> GetByIdNoTracking(UserId userId, CancellationToken ct);

    Task<User?> GetByEmailNoTracking(string email, CancellationToken ct);

    Task Add(User user, CancellationToken ct);
}
