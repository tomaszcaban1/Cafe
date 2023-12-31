using Cafe.Application.Common.Interfaces.Persistence;
using Cafe.Domain.Aggregates.HostAggregate;
using Cafe.Domain.Aggregates.MenuAggregate.Events;
using Cafe.Domain.Aggregates.MenuAggregate.ValueObjects;
using MediatR;

namespace Cafe.Application.Hosts.Events;

public class MenuCreatedHandler : INotificationHandler<MenuCreated>
{
    readonly IHostRepository _hostRepository;

    public MenuCreatedHandler(IHostRepository hostRepository)
    {
        _hostRepository = hostRepository;
    }

    public async Task Handle(MenuCreated notification, CancellationToken ct)
    {
        if (await _hostRepository.GetById(notification.Menu.HostId, ct) is not Host host)
        {
            throw new InvalidOperationException($"Menu has invalid host id: {notification.Menu.HostId.Value}");
        }

        host.AddMenuId((MenuId)notification.Menu.Id);
        await _hostRepository.Update(host, ct);
    }
}
