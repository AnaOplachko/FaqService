using FaqDataAccess.Repositories.ArticleRepository;
using FaqService.Application.Exceptions;
using MediatR;

namespace FaqService.Application.Commands.Admin.Article.ReadById;

/// <summary>
/// Обработчик запроса на получение статьи по идентификатору
/// </summary>
public class GetArticleByIdQueryHandler: IRequestHandler<GetArticleByIdQuery, Dtos.Article>
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
    public async Task<Dtos.Article> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetArticleByIdAsync(request.Id);

        if (article == null!)
            throw new NoEntityException("No article found");

        return new Dtos.Article(article);
    }
}