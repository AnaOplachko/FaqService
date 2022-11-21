using FaqDataAccess.Repositories.ArticleRepository;
using FaqDomain.Events;
using MediatR;

namespace FaqService.Application.DomainEventHandlers;

/// <summary>
/// Обработчик события Article Removed From Section Event
/// </summary>
public class ArticleRemovedFromSectionEventHandler : INotificationHandler<ArticleRemovedFromSectionEvent>
{
    private readonly IArticleRepository _articleRepository;

    /// <summary>
    /// ctor
    /// </summary>
    public ArticleRemovedFromSectionEventHandler(IArticleRepository articleRepository) => _articleRepository = articleRepository;

    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task Handle(ArticleRemovedFromSectionEvent notification, CancellationToken cancellationToken)
    {
        if (notification.OrderPosition is not null)
            await _articleRepository.UpdatePositionsAfterArticleDeleted(notification.Id, notification.SectionId);
    }
}