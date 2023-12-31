using Cafe.Application.Menus.Commands.CreateMenu;
using Cafe.Contracts.Menu;
using Cafe.Domain.Aggregates.MenuAggregate;
using Mapster;
using MenuSection = Cafe.Domain.Aggregates.MenuAggregate.Entities.MenuSection;
using MenuItem = Cafe.Domain.Aggregates.MenuAggregate.Entities.MenuItem;
using Cafe.Contracts.Common;
using Cafe.Application.Menus.Queries.GetAllMenusPaged;
using Cafe.Application.Common;

namespace Cafe.Api.Common.Mapping;

public class MenuMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreateMenuRequest Request, string HostId), CreateMenuCommand>()
            .Map(dest => dest.HostId, src => src.HostId)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<Menu, MenuResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString())
            .Map(dest => dest.HostId, src => src.HostId.Value.ToString());

        config.NewConfig<MenuSection, MenuSectionResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString());

        config.NewConfig<MenuItem, MenuItemResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString())
            .Map(dest => dest.Price, src => src.ItemPrice.Amount.ToString())
            .Map(dest => dest.Currency, src => src.ItemPrice.Currency);

        config.NewConfig<PagedRequest, GetAllMenusPagedQuery>();

        config.NewConfig<PagedResult<Menu>, PagedResponse<Menu>>();
    }
}
