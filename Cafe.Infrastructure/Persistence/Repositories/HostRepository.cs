using Cafe.Application.Common.Interfaces.Persistence;
using Cafe.Domain.Aggregates.HostAggregate;
using Cafe.Domain.Aggregates.HostAggregate.ValueObjects;
using Cafe.Domain.Aggregates.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Infrastructure.Persistence.Repositories;

public class HostRepository : IHostRepository
{
    readonly CafeDbContext _dbContext;

    public HostRepository(CafeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> IsUserIdUniqueUdentifier(UserId userId, CancellationToken ct) => 
        _dbContext.Hosts.AnyAsync(x => x.UserId == userId, ct);

    public Task<List<Host>> GetAllNoTracking(CancellationToken ct) => 
        _dbContext.Hosts
                .AsNoTracking()
                .ToListAsync(ct);

    public Task<Host?> GetById(HostId id, CancellationToken ct) => 
        _dbContext.Hosts.FirstOrDefaultAsync(x => x.Id == id, ct);

    public Task Add(Host host, CancellationToken ct)
    {
        _dbContext.Hosts.Add(host);
        return _dbContext.SaveChangesAsync(ct);        
    }

    public Task Update(Host host, CancellationToken ct)
    {
        _dbContext.Hosts.Update(host);
        return _dbContext.SaveChangesAsync(ct);
    }
}
