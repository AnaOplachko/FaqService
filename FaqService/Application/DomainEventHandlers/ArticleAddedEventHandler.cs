using FaqDataAccess.Repositories.ArticleRepository;
using FaqDomain.Events;
using MediatR;

namespace FaqService.Application.DomainEventHandlers;

/// <summary>
/// Обработчик события Article Added Event
/// </summary>
public class ArticleAddedEventHandler : INotificationHandler<ArticleAddedEvent>
{
    private readonly IArticleRepository _articleRepository;

    /// <summary>
    /// ctor
    /// </summary>
    public ArticleAddedEventHandler(IArticleRepository articleRepository) => _articleRepository = articleRepository;
    
    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task Handle(ArticleAddedEvent notification, CancellationToken cancellationToken)
    {
        if (notification.OrderPosition is not null)
            await _articleRepository.UpdatePositionsAfterArticleAdded(notification.SectionId, (int)notification.OrderPosition);
    }
}