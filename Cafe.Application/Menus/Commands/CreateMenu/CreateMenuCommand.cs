using Cafe.Application.Common.Interfaces.Behaviors;
using ErrorOr;
using Cafe.Domain.Aggregates.MenuAggregate;

namespace Cafe.Application.Menus.Commands.CreateMenu;

public record CreateMenuCommand(string HostId,
                                string Name,
                                string Description,
                                List<CreateMenuSectionCommand> Sections) : IValidatableRequest<ErrorOr<Menu>>;

public record CreateMenuSectionCommand(string Name,
                                         string Description,
                                         List<CreateMenuItemCommand> Items);

public record CreateMenuItemCommand(string Name,
                                    string Description,
                                    CreateMenuItemPriceCommand ItemPrice);

public record CreateMenuItemPriceCommand(decimal Amount, string Currency);
