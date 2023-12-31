using Cafe.Application.Common.Interfaces.Persistence;
using Cafe.Domain.Aggregates.HostAggregate.ValueObjects;
using Cafe.Domain.Aggregates.MenuAggregate;
using Cafe.Domain.Aggregates.MenuAggregate.Entities;
using Cafe.Domain.Common.ValueObjects;
using ErrorOr;
using MediatR;

namespace Cafe.Application.Menus.Commands.CreateMenu;

public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, ErrorOr<Menu>>
{
    readonly IMenuRepository _menuRepository;

    public CreateMenuCommandHandler(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<ErrorOr<Menu>> Handle(CreateMenuCommand request, CancellationToken ct)
    {
        var menu = Menu.Create(
            hostId: HostId.Create(request.HostId),
            name: request.Name,
            description: request.Description,
            sections: request.Sections.ConvertAll(section => MenuSection.Create(
                name: section.Name,
                description: section.Description,
                items: section.Items.ConvertAll(item => MenuItem.Create(
                    name: item.Name,
                    description: item.Description,
                    price: Price.Create(
                        amount: item.ItemPrice.Amount,
                        currency: item.ItemPrice.Currency
                    )
                )))));

        await _menuRepository.Add(menu, ct);

        return menu;
    }
}