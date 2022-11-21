using MediatR;

namespace FaqDomain.Events;

/// <summary>
/// Событие вызывающее обновление позиций сортировки для всех статей категории
/// </summary>
public class ArticleChangedOrderPositionEvent : INotification
{
    /// <summary>
    /// ctor
    /// </summary>
    public ArticleChangedOrderPositionEvent(int id, int sectionId, int? orderPosition)
    {
        SectionId = sectionId;
        OrderPosition = orderPosition;
        Id = id;
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
    /// Идентификатор статьи
    /// </summary>
    public int? OrderPosition { get; }
}