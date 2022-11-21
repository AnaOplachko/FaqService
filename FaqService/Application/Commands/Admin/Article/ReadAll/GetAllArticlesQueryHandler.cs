using FaqDataAccess.Repositories.ArticleRepository;
using FaqService.Application.Exceptions;
using FaqService.Application.Models;
using MediatR;

namespace FaqService.Application.Commands.Admin.Article.ReadAll;

/// <summary>
/// Обработчик запроса на получение всех статей
/// </summary>
public class GetAllArticlesQueryHandler : IRequestHandler<GetAllArticlesQuery, List<ArticleModel>>
{
    private readonly IArticleRepository _articleRepository;

    /// <summary>
    /// ctor
    /// </summary>
    public GetAllArticlesQueryHandler(IArticleRepository articleRepository)
        => _articleRepository = articleRepository;

    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task<List<ArticleModel>> Handle(GetAllArticlesQuery request, CancellationToken cancellationToken)
    {
        var articles = await _articleRepository.GetAllArticlesAsync();

        if (articles.Count is 0)
            throw new NoEntityException("No articles found");
        
        return articles.Select(x=> new ArticleModel(x)).ToList();
    }
}