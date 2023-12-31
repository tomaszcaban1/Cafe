using Cafe.Application.Common.Extensions;
using Cafe.Application.Common.Interfaces.Persistence;
using Cafe.Domain.Aggregates.HostAggregate;
using Cafe.Domain.Aggregates.UserAggregate.ValueObjects;
using ErrorOr;
using MediatR;

using Errors = Cafe.Domain.Common.Errors.Errors;

namespace Cafe.Application.Hosts.Commands;

public class CreateHostCommandHandler : IRequestHandler<CreateHostCommand, ErrorOr<Host>>
{
    readonly IHostRepository _hostRepository;

    public CreateHostCommandHandler(IHostRepository hostRepository)
    {
        _hostRepository = hostRepository;
    }

    public async Task<ErrorOr<Host>> Handle(CreateHostCommand request, CancellationToken ct)
    {
        var userIdGuid = request.UserId.ParseToGuid();
        var userId = UserId.Create(userIdGuid);

        if (await _hostRepository.IsUserIdUniqueUdentifier(userId, ct))
        {
            return Errors.Registration.UserNotUnique;
        }  

        var host = Host.Create(userId, request.Name);

        await _hostRepository.Add(host, ct);

        return host;
    }
}
