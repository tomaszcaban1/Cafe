using Cafe.Application.Common.Interfaces.Behaviors;
using Cafe.Domain.Aggregates.MenuAggregate;
using ErrorOr;

namespace Cafe.Application.Menus.Queries.GetAllMenus;

public record GetAllMenusQuery() : IValidatableRequest<ErrorOr<List<Menu>>>;
