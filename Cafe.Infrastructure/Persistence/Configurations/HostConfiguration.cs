using Cafe.Domain.Aggregates.HostAggregate;
using Cafe.Domain.Aggregates.HostAggregate.ValueObjects;
using Cafe.Domain.Aggregates.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cafe.Infrastructure.Persistence.Configurations;

public class HostConfiguration : IEntityTypeConfiguration<Host>
{
    public void Configure(EntityTypeBuilder<Host> builder)
    {
        configureHostsTable(builder);
        configureHostMenuIdsTable(builder);
    }

    static void configureHostsTable(EntityTypeBuilder<Host> builder)
    {
        builder.ToTable("Host");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => HostId.Create(value));

        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.UserId)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));
    }

    static void configureHostMenuIdsTable(EntityTypeBuilder<Host> builder)
    {
        builder.OwnsMany(h => h.MenuIds, mib =>
        {
            mib.WithOwner().HasForeignKey("HostId");

            mib.ToTable("HostMenuIds");

            mib.HasKey("Id");

            mib.Property(mi => mi.Value)
                .ValueGeneratedNever()
                .HasColumnName("HostMenuId");
        });

        builder.Metadata.FindNavigation(nameof(Host.MenuIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
