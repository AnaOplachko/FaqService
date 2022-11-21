using FaqDataAccess.Repositories.ArticleRepository;
using FaqService.Application.Exceptions;
using MediatR;

namespace FaqService.Application.Commands.Admin.Article.Delete;

/// <summary>
/// Обработчик команды на удаление статьи
/// </summary>
public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand>
{
    private readonly IArticleRepository _articleRepository;

    /// <summary>
    /// ctor
    /// </summary>
    public DeleteArticleCommandHandler(IArticleRepository articleRepository)
        => _articleRepository = articleRepository;
    
    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task<Unit> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetArticleByIdAsync(request.Id);

        if (article == null!)
            throw new NoEntityException("No article found");
         
        article.MarkForDelete();
        _articleRepository.DeleteArticle(article);
        await _articleRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return Unit.Value;
    }
}