using Cafe.Application.Common.Interfaces.Behaviors;
using Cafe.Domain.Aggregates.HostAggregate;
using ErrorOr;

namespace Cafe.Application.Hosts.Queries.GetAllHosts;

public record GetAllHostsQuery : IValidatableRequest<ErrorOr<List<Host>>>;
