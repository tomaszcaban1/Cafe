using Cafe.Application.Common.Interfaces.Persistence;
using Cafe.Domain.Aggregates.MenuAggregate;
using Cafe.Domain.Aggregates.MenuReviewAggregate.Events;
using Cafe.Domain.Aggregates.MenuReviewAggregate.ValueObjects;
using MediatR;

namespace Cafe.Application.Menus.Events;

public class MenuReviewCreatedHandler : INotificationHandler<MenuReviewCreated>
{
    readonly IMenuRepository _menuRepository;

    public MenuReviewCreatedHandler(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task Handle(MenuReviewCreated notification, CancellationToken ct)
    {
        if (await _menuRepository.GetById(notification.MenuReview.MenuId, ct) is not Menu menu)
        {
            throw new InvalidOperationException($"Menu review has invalid menu id: {notification.MenuReview.MenuId.Value}");
        }

        menu.AddMenuReviewId((MenuReviewId)notification.MenuReview.Id);
        await _menuRepository.Update(menu, ct);
    }
}