using MediatR;

namespace FaqDomain.Events;

/// <summary>
/// Событие вызывающее обновление позиций сортировки для всех статей категории
/// </summary>
public class ArticleAddedEvent : INotification
{
    /// <summary>
    /// ctor
    /// </summary>
    public ArticleAddedEvent(int sectionId, int? orderPosition)
    {
        SectionId = sectionId;
        OrderPosition = orderPosition;
    }

    /// <summary>
    /// Идентификатор родительской категории
    /// </summary>
    public int SectionId { get; }
    
    /// <summary>
    /// Позиция новой статьи в списке
    /// </summary>
    public int? OrderPosition { get; }
}