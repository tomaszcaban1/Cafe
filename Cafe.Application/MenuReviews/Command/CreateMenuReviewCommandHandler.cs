using ErrorOr;
using MediatR;
using Cafe.Domain.Aggregates.MenuReviewAggregate;
using Cafe.Application.Common.Interfaces.Persistence;
using Cafe.Domain.Aggregates.UserAggregate.ValueObjects;
using Cafe.Domain.Aggregates.MenuAggregate.ValueObjects;
using Cafe.Domain.Aggregates.MenuAggregate;
using Errors = Cafe.Domain.Common.Errors.Errors;
using Cafe.Application.Common.Extensions;

namespace Cafe.Application.MenuReviews.Command;

public class CreateMenuReviewCommandHandler : IRequestHandler<CreateMenuReviewCommand, ErrorOr<MenuReview>>
{
    readonly IMenuReviewRepository _menuReviewRepository;
    readonly IMenuRepository _menuRepository;
    readonly IUserRepository _userRepository;

    public CreateMenuReviewCommandHandler(
        IMenuReviewRepository menuReviewRepository, 
        IMenuRepository menuRepository, 
        IUserRepository userRepository)
    {
        _menuReviewRepository = menuReviewRepository;
        _menuRepository = menuRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<MenuReview>> Handle(CreateMenuReviewCommand request, CancellationToken ct)
    {
        var userIdGuid = request.UserId.ParseToGuid();
        var userId = UserId.Create(userIdGuid);

        var menuIdGuid = request.MenuId.ParseToGuid();
        var menuId = MenuId.Create(menuIdGuid);

        if (await (_menuRepository.GetByIdNoTrancking(menuId, ct)) is not Menu menu)
        {
            return Errors.Menu.MissingMenuId;
        }

        if (await (_userRepository.GetByIdNoTracking(userId, ct)) is null)
        {
            return Errors.User.MissingUserId;
        }

        var hostId = menu.HostId;

        var menuReview = MenuReview.Create(
                                rating: request.RatingValue,
                                comment: request.Comment,
                                hostId: hostId,
                                userId: userId,
                                menuId: menuId);

        await _menuReviewRepository.AddAsync(menuReview, ct);

        return menuReview;
    }
}
