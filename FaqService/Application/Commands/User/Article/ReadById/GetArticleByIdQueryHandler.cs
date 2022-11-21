using FaqDataAccess.Repositories.ArticleRepository;
using FaqService.Application.Exceptions;
using FaqService.Application.Models;
using MediatR;

namespace FaqService.Application.Commands.User.Article.ReadById;

/// <summary>
/// Обработчик запроса на получение пользователем статьи по идентификатору
/// </summary>
public class GetArticleByIdQueryHandler : IRequestHandler<GetArticleByIdQuery, ArticleModel>
{
    private readonly ICachedArticleRepository _articleCachedRepository;

    /// <summary>
    /// ctor
    /// </summary>
    public GetArticleByIdQueryHandler(ICachedArticleRepository articleCachedRepository)
        => _articleCachedRepository = articleCachedRepository;
    
    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task<ArticleModel> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
    {
        var article = await _articleCachedRepository.GetArticleByIdAsync(request.Id);

        if (article == null!)
            throw new NoEntityException("No article found");
        
        return new ArticleModel(article);
    }
}