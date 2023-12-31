using Cafe.Application.Common.Interfaces.Persistence;
using Cafe.Application.Menus.Queries.GetAllMenusPaged;
using Cafe.Domain.Aggregates.MenuAggregate;
using Cafe.Domain.Aggregates.MenuAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cafe.Infrastructure.Persistence.Repositories;

public class MenuRepository : IMenuRepository
{
    readonly CafeDbContext _dbContext;

    public MenuRepository(CafeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<Menu>> GetAllNoTracking(CancellationToken ct) => 
        _dbContext.Menus
                .AsNoTracking()
                .ToListAsync(ct);

    public async Task<(List<Menu>, int)> GetPagedResultNoTracking(GetAllMenusPagedQuery pageRequest, CancellationToken ct)
    {
        var filter = pageRequest.Filter;

        var query = _dbContext.Menus
                        .Where(x => filter == null ||
                            (x.Name.ToLower().Contains(filter.ToLower()) || 
                             x.Description.ToLower().Contains(filter.ToLower())));

        var totalCount = await query.CountAsync(ct);

        if (pageRequest.SortBy is not null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Menu, object>>>
                {
                    { nameof(Menu.Name), x => x.Name },
                    { nameof(Menu.Description), x => x.Description },
            };

            var sortByExpression = columnsSelector[pageRequest.SortBy];

            query = pageRequest.SortByDesc
                ? query.OrderByDescending(sortByExpression)
                : query.OrderBy(sortByExpression);
        }

        var users = await query
                .Skip(pageRequest.PageSize * (pageRequest.PageNumber - 1))
                .Take(pageRequest.PageSize)
                .AsNoTracking()
                .ToListAsync(ct);

        return (users, totalCount);
    }

    public Task<Menu?> GetById(MenuId menuId, CancellationToken ct) =>
        _dbContext.Menus.FirstOrDefaultAsync(menu => menu.Id == menuId, ct);

    public Task<Menu?> GetByIdNoTrancking(MenuId menuId, CancellationToken ct) =>
        _dbContext.Menus
                    .AsNoTracking()
                    .FirstOrDefaultAsync(menu => menu.Id == menuId, ct);

    public Task Add(Menu menu, CancellationToken ct)
    {
        _dbContext.Menus.Add(menu);
        return _dbContext.SaveChangesAsync(ct);
    }

    public Task Update(Menu menu, CancellationToken ct)
    {
        _dbContext.Menus.Update(menu);
        return _dbContext.SaveChangesAsync(ct);
    }
}
