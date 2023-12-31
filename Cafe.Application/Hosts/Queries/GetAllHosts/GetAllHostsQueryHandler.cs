using Cafe.Application.Common.Interfaces.Persistence;
using Cafe.Domain.Aggregates.HostAggregate;
using ErrorOr;
using MediatR;

namespace Cafe.Application.Hosts.Queries.GetAllHosts;

public class GetAllHostsQueryHandler : IRequestHandler<GetAllHostsQuery, ErrorOr<List<Host>>>
{
    readonly IHostRepository _hostRepository;

    public GetAllHostsQueryHandler(IHostRepository hostRepository)
    {
        _hostRepository = hostRepository;
    }

    public async Task<ErrorOr<List<Host>>> Handle(GetAllHostsQuery request, CancellationToken ct) => 
        await _hostRepository.GetAllNoTracking(ct);
}
