using MediatR;

namespace FaqDomain.Events;

/// <summary>
/// Событие вызывающее отправку сообщения в брокер при добавлении категории
/// </summary>
public class SectionAddedEvent : INotification
{
    /// <summary>
    /// ctor
    /// </summary>
    public SectionAddedEvent(string message) => Message = message;
    
    /// <summary>
    /// Сообщение
    /// </summary>
    public string Message { get; }
}