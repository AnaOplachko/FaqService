using FaqDataAccess.Repositories.ArticleRepository;
using FaqService.Application.Exceptions;
using FaqService.Application.Models;
using MediatR;

namespace FaqService.Application.Commands.Admin.Article.ReadById;

/// <summary>
/// Обработчик запроса на получение статьи по идентификатору
/// </summary>
public class GetArticleByIdQueryHandler: IRequestHandler<GetArticleByIdQuery, ArticleModel>
{
    private readonly IArticleRepository _articleRepository;

    /// <summary>
    /// ctor
    /// </summary>
    public GetArticleByIdQueryHandler(IArticleRepository articleRepository)
        => _articleRepository = articleRepository;

    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task<ArticleModel> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetArticleByIdAsync(request.Id);

        if (article == null!)
            throw new NoEntityException("No article found");

        return new ArticleModel(article);
    }
}