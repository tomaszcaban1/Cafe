using Cafe.Application.Common;
using Cafe.Application.Common.Interfaces.Behaviors;
using Cafe.Domain.Aggregates.MenuAggregate;
using ErrorOr;

namespace Cafe.Application.Menus.Queries.GetAllMenusPaged;

public record GetAllMenusPagedQuery(
    string Filter,
    string SortBy,
    bool SortByDesc,
    int PageNumber,
    int PageSize) : IValidatableRequest<ErrorOr<PagedResult<Menu>>>;
