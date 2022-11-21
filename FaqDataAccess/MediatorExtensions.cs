using FaqDomain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FaqDataAccess;

static class MediatorExtensions
{
    /// <summary>
    /// Публикует доменные события
    /// </summary>
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, DbContext context)
    {
        var domainEntities = context
            .ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents.Count != 0)
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}