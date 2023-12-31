using Cafe.Domain.Aggregates.HostAggregate.ValueObjects;
using Cafe.Domain.Aggregates.MenuAggregate;
using Cafe.Domain.Aggregates.MenuAggregate.Entities;
using Cafe.Domain.Aggregates.MenuAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cafe.Infrastructure.Persistence.Configurations;

public class MenuConfiguration : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        configureMenusTable(builder);
        configureMenuSectionsTable(builder);
        configureMenuReviewIdsTable(builder);
    }

    static void configureMenusTable(EntityTypeBuilder<Menu> builder)
    {
        builder.ToTable("Menu");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => MenuId.Create(value));

        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(x => x.HostId)
            .HasConversion(
                id => id.Value,
                value => HostId.Create(value));
    }

    static void configureMenuSectionsTable(EntityTypeBuilder<Menu> builder)
    {
        builder.OwnsMany(x => x.Sections, sb =>
        {
            sb.ToTable("MenuSection");

            sb.WithOwner().HasForeignKey("MenuId");

            sb.HasKey("Id", "MenuId");

            sb.Property(x => x.Id)
                .HasColumnName("MenuSectionId")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => MenuSectionId.Create(value));

            sb.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            sb.Property(x => x.Description)
                .HasMaxLength(250)
                .IsRequired();

            sb.OwnsMany(x => x.Items, ib =>
            {
                ib.ToTable("MenuItem");

                ib.WithOwner().HasForeignKey("MenuSectionId", "MenuId");

                ib.HasKey(nameof(MenuItem.Id), "MenuSectionId", "MenuId");

                ib.Property(x => x.Id)
                    .HasColumnName("MenuItemId")
                    .ValueGeneratedNever()
                    .HasConversion(
                        id => id.Value,
                        value => MenuItemId.Create(value));

                ib.Property(x => x.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                ib.Property(x => x.Description)
                    .HasMaxLength(250)
                    .IsRequired();

                ib.OwnsOne(x => x.ItemPrice, p =>
                {
                    p.Property(x => x.Amount)
                        .HasColumnType("decimal(10,4)");
                });
            });

            sb.Navigation(x => x.Items).Metadata.SetField("_items");
            sb.Navigation(x => x.Items).UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        builder.Metadata.FindNavigation(nameof(Menu.Sections))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    static void configureMenuReviewIdsTable(EntityTypeBuilder<Menu> builder)
    {
        builder.OwnsMany(m => m.MenuReviewIds, dib =>
        {
            dib.ToTable("MenuReviewIds");

            dib.WithOwner().HasForeignKey("MenuId");

            dib.HasKey("Id");

            dib.Property(d => d.Value)
                .HasColumnName("ReviewId")
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Menu.MenuReviewIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
