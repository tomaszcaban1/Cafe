using Cafe.Application.Common;
using Cafe.Application.Common.Interfaces.Persistence;
using Cafe.Domain.Aggregates.MenuAggregate;
using ErrorOr;
using MediatR;

namespace Cafe.Application.Menus.Queries.GetAllMenusPaged;

public class GetAllMenusPagedQueryHandler : IRequestHandler<GetAllMenusPagedQuery, ErrorOr<PagedResult<Menu>>>
{
    readonly IMenuRepository _menuRepository;

    public GetAllMenusPagedQueryHandler(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<ErrorOr<PagedResult<Menu>>> Handle(GetAllMenusPagedQuery request, CancellationToken ct)
    {
        var menus = await _menuRepository.GetPagedResultNoTracking(request, ct);

        return new PagedResult<Menu>(menus.Item1, menus.Item2, request.PageSize, request.PageNumber);
    }
}
