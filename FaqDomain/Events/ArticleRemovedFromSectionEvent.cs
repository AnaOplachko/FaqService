using MediatR;

namespace FaqDomain.Events;

/// <summary>
/// Событие вызывающее обновление позиций сортировки для всех статей категории
/// </summary>
public class ArticleRemovedFromSectionEvent : INotification
{
    /// <summary>
    /// ctor
    /// </summary>
    public ArticleRemovedFromSectionEvent(int id, int sectionId, int? orderPosition)
    {
        Id = id;
        SectionId = sectionId;
        OrderPosition = orderPosition;
    }

    /// <summary>
    /// Идентификатор статьи
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Идентификатор родительской категории
    /// </summary>
    public int SectionId { get; }

    /// <summary>
    /// Позиция статьи
    /// </summary>
    public int? OrderPosition { get; }
}