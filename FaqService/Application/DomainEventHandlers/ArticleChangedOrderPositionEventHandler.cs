using FaqDataAccess.Repositories.ArticleRepository;
using FaqDomain.Events;
using MediatR;

namespace FaqService.Application.DomainEventHandlers;

/// <summary>
/// Обработчик события Article Changed Order Position Event
/// </summary>
public class ArticleChangedOrderPositionEventHandler : INotificationHandler<ArticleChangedOrderPositionEvent>
{
    private readonly IArticleRepository _articleRepository;
    
    /// <summary>
    /// ctor
    /// </summary>
    public ArticleChangedOrderPositionEventHandler(IArticleRepository articleRepository) => _articleRepository = articleRepository;
    
    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task Handle(ArticleChangedOrderPositionEvent notification, CancellationToken cancellationToken)
    {
        await _articleRepository.UpdatePositionsAfterArticleModified(notification.Id, notification.SectionId,
            notification.OrderPosition);
        await _articleRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}