using MediatR;

namespace FaqDomain;

public abstract class Entity
{
    private readonly List<INotification> _domainEvents = new();

    /// <summary>
    /// Коллекция доменных событий
    /// </summary>
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Добавляет доменное событие
    /// </summary>
    protected void AddDomainEvent(INotification eventItem) => _domainEvents.Add(eventItem);

    /// <summary>
    /// Удалить доменное событие
    /// </summary>
    public void RemoveDomainEvent(INotification eventItem) => _domainEvents.Remove(eventItem);

    /// <summary>
    /// Очистить коллекцию доменных событий
    /// </summary>
    public void ClearDomainEvents() => _domainEvents.Clear();
}