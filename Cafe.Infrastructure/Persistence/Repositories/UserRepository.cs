using Cafe.Application.Common.Interfaces.Persistence;
using Cafe.Domain.Aggregates.UserAggregate;
using Cafe.Domain.Aggregates.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    readonly CafeDbContext _dbContext;

    public UserRepository(CafeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> IsEmailTaken(string email, CancellationToken ct) => 
        _dbContext.Users.AnyAsync(u => u.Email == email, ct);

    public Task<User?> GetByIdNoTracking(UserId id, CancellationToken ct) => 
        _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id, ct);

    public Task<User?> GetByEmailNoTracking(string email, CancellationToken ct) =>
        _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email, ct);

    public Task Add(User user, CancellationToken ct)
    {
        _dbContext.Users.Add(user);
        return _dbContext.SaveChangesAsync(ct);
    }
}
