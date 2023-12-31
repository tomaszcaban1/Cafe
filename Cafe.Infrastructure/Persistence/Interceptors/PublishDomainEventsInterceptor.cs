using Cafe.Domain.Common.Models.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Cafe.Infrastructure.Persistence.Interceptors;

public class PublishDomainEventsInterceptor : SaveChangesInterceptor
{
    readonly IPublisher _mediator;

    public PublishDomainEventsInterceptor(IPublisher mediator)
    {
        _mediator = mediator;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        publishDomainEvents(eventData.Context).GetAwaiter().GetResult();

        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result, 
        CancellationToken cancellationToken = default)
    {
        await publishDomainEvents(eventData.Context);

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    async Task publishDomainEvents(DbContext? dbContext)
    {
        if(dbContext is null)
        {
            return;
        }

        //get hold of all the various entities
        var entitiesWithDomainEvents = dbContext.ChangeTracker.Entries<IHasDomainEvents>()
            .Where(x => x.Entity.DomainEvents.Any())
            .Select(x => x.Entity)
            .ToList();
        
        //get hold of all the domain events
        var domainEvents = entitiesWithDomainEvents
            .SelectMany(x => x.DomainEvents)
            .ToList();

        //Clear domain events
        entitiesWithDomainEvents.ForEach(x => x.ClearDomainEvents());

        //Publish domain events
        foreach (var domainEvent in domainEvents)
        {
            await _mediator.Publish(domainEvent);
        }
     }
}
