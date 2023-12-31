using Cafe.Application.Menus.Queries.GetAllMenusPaged;
using Cafe.Domain.Aggregates.MenuAggregate;
using Cafe.Domain.Aggregates.MenuAggregate.ValueObjects;

namespace Cafe.Application.Common.Interfaces.Persistence;

public interface IMenuRepository
{
    Task<List<Menu>> GetAllNoTracking(CancellationToken ct);

    Task<(List<Menu>, int)> GetPagedResultNoTracking(GetAllMenusPagedQuery getAllMenusPagedQuery, CancellationToken ct);

    Task<Menu?> GetById(MenuId menuId, CancellationToken ct);

    Task<Menu?> GetByIdNoTrancking(MenuId menuId, CancellationToken ct);

    Task Add(Menu menu, CancellationToken ct);

    Task Update(Menu menu, CancellationToken ct);
}
