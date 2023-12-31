using Cafe.Domain.Aggregates.HostAggregate;
using Cafe.Domain.Aggregates.MenuAggregate;
using Cafe.Domain.Aggregates.MenuReviewAggregate;
using Cafe.Domain.Aggregates.UserAggregate;
using Cafe.Domain.Common.Models.Interfaces;
using Cafe.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Infrastructure.Persistence;

public class CafeDbContext : DbContext
{
    readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;
    public CafeDbContext(DbContextOptions<CafeDbContext> options, PublishDomainEventsInterceptor publishDomainEventsInterceptor) 
        : base(options)
    {
        _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Host> Hosts { get; set; }

    public DbSet<Menu> Menus { get; set; }

    public DbSet<MenuReview> MenuReviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Ignore<List<IDomainEvent>>()
            .ApplyConfigurationsFromAssembly(typeof(CafeDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);

        base.OnConfiguring(optionsBuilder);
    }
}
