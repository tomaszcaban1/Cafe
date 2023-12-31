using Cafe.Domain.Aggregates.HostAggregate.ValueObjects;
using Cafe.Domain.Aggregates.MenuAggregate.ValueObjects;
using Cafe.Domain.Aggregates.MenuReviewAggregate;
using Cafe.Domain.Aggregates.MenuReviewAggregate.ValueObjects;
using Cafe.Domain.Aggregates.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cafe.Infrastructure.Persistence.Configurations;

public class MenuReviewConfiguration : IEntityTypeConfiguration<MenuReview>
{
    public void Configure(EntityTypeBuilder<MenuReview> builder)
    {
        builder.ToTable("MenuReview");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => MenuReviewId.Create(value));

        builder.OwnsOne(x => x.Rating);

        builder.Property(x => x.Comment)
            .HasMaxLength(500);

        builder.Property(x => x.HostId)
            .HasConversion(
                id => id.Value,
                value => HostId.Create(value));

        builder.Property(x => x.UserId)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));

        builder.Property(x => x.MenuId)
            .HasConversion(
                id => id.Value,
                value => MenuId.Create(value));
    }
}
