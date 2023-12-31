using Cafe.Application.Common.Interfaces.Persistence;
using Cafe.Domain.Aggregates.MenuAggregate;
using ErrorOr;
using MediatR;

namespace Cafe.Application.Menus.Queries.GetAllMenus;

public class GetAllMenusQueryHandler : IRequestHandler<GetAllMenusQuery, ErrorOr<List<Menu>>>
{
    readonly IMenuRepository _menuRepository;

    public GetAllMenusQueryHandler(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<ErrorOr<List<Menu>>> Handle(GetAllMenusQuery request, CancellationToken cancellationToken) => 
        await _menuRepository.GetAllNoTracking(cancellationToken);
}
