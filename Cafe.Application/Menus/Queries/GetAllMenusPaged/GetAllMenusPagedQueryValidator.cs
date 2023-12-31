using Cafe.Application.Menus.Queries.GetAllMenus;
using FluentValidation;

namespace Cafe.Application.Menus.Queries.GetAllMenusPaged;

public class GetAllMenusPagedQueryValidator : AbstractValidator<GetAllMenusPagedQuery>
{
    public GetAllMenusPagedQueryValidator()
    {
    }
}
