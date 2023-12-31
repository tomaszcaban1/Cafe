using Cafe.Application.MenuReviews.Command;
using Cafe.Contracts.MenuReview;
using Cafe.Domain.Aggregates.MenuReviewAggregate;
using Mapster;

namespace Cafe.Api.Common.Mapping;

public class MenuReviewMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreateMenuReviewRequest Request, string UserId, string MenuId), CreateMenuReviewCommand>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.MenuId, src => src.MenuId)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<MenuReview, MenuReviewResponse>()
            .Map(dest => dest.Id, src => src.Id.Value.ToString())
            .Map(dest => dest.RatingValue, src => src.Rating.Value)
            .Map(dest => dest.Comment, src => src.Comment)
            .Map(dest => dest.UserId, src => src.UserId.Value)
            .Map(dest => dest.MenuId, src => src.MenuId.Value);
    }
}
