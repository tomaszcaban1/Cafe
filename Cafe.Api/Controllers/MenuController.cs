using Cafe.Application.Common;
using Cafe.Application.Menus.Commands.CreateMenu;
using Cafe.Application.Menus.Queries.GetAllMenus;
using Cafe.Application.Menus.Queries.GetAllMenusPaged;
using Cafe.Contracts.Common;
using Cafe.Contracts.Menu;
using Cafe.Domain.Aggregates.MenuAggregate;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.Api.Controllers;

[Route("api/[controller]")]
public class MenuController : ApiBaseController
{
    readonly ISender _mediator;
    readonly IMapper _mapper;

    public MenuController(ISender mediator, IMapper mapper) : base(mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetAll()
    {
        var getAllMenuQuery = new GetAllMenusQuery();
        var getAllMenuResult = await _mediator.Send(getAllMenuQuery);

        return MatchResultGet<List<Menu>, List<MenuResponse>>(getAllMenuResult);
    }

    [HttpGet("paged_list")]
    public async Task<IActionResult> GetAllPaged([FromQuery]PagedRequest request)
    {
        var query = _mapper.Map<GetAllMenusPagedQuery>(request);
        var getAllMenuPagedResult = await _mediator.Send(query);

        return MatchResultGet<PagedResult<Menu>, PagedResponse<MenuResponse>>(getAllMenuPagedResult);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMenuRequest request, string hostId)
    {
        var command = _mapper.Map<CreateMenuCommand>((request, hostId));
        var createMenuResult = await _mediator.Send(command);

        return MatchResultPost<Menu, MenuResponse>(createMenuResult, "register");
    }
}
