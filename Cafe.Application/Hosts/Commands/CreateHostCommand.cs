using Cafe.Application.Common.Interfaces.Behaviors;
using Cafe.Domain.Aggregates.HostAggregate;
using ErrorOr;

namespace Cafe.Application.Hosts.Commands;

public record CreateHostCommand(string UserId, string Name) : IValidatableRequest<ErrorOr<Host>>;