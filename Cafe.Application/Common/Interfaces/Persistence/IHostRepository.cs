using Cafe.Domain.Aggregates.HostAggregate;
using Cafe.Domain.Aggregates.HostAggregate.ValueObjects;
using Cafe.Domain.Aggregates.UserAggregate.ValueObjects;

namespace Cafe.Application.Common.Interfaces.Persistence;

public interface IHostRepository
{
    Task<List<Host>> GetAllNoTracking(CancellationToken ct);

    Task<bool> IsUserIdUniqueUdentifier(UserId userId, CancellationToken ct);

    Task<Host?> GetById(HostId id, CancellationToken ct);

    Task Add(Host host, CancellationToken ct);

    Task Update(Host host, CancellationToken ct);
}
