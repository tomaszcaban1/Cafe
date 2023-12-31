using Cafe.Application.MenuReviews.Command;
using Cafe.Contracts.MenuReview;
using Cafe.Domain.Aggregates.MenuReviewAggregate;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.Api.Controllers;

[Route("api/menus/{menuId}/[controller]")]
public class MenuReviewController : ApiBaseController
{
    readonly ISender _mediator;
    readonly IMapper _mapper;

    public MenuReviewController(ISender mediator, IMapper mapper) : base(mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMenuReviewRequest request, string userId, string menuId)
    {
        var command = _mapper.Map<CreateMenuReviewCommand>((request, userId, menuId));
        var createMenuReviewResult = await _mediator.Send(command);

        return MatchResultPost<MenuReview, MenuReviewResponse>(createMenuReviewResult, "createdMenuReview");
    }
}
