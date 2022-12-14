using FaqDataAccess.Repositories.ArticleRepository;
using FaqService.Application.Exceptions;
using MediatR;

namespace FaqService.Application.Commands.User.Article.ReadAll;

/// <summary>
/// Обработчик запроса на получение пользователем списка всех статей
/// </summary>
public class GetAllArticlesQueryHandler : IRequestHandler<GetAllArticlesQuery, List<Dtos.Article>>
{
    private readonly ICachedArticleRepository _articleCachedRepository;

    /// <summary>
    /// ctor
    /// </summary>
    public GetAllArticlesQueryHandler(ICachedArticleRepository articleCachedRepository)
        => _articleCachedRepository = articleCachedRepository;
    
    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task<List<Dtos.Article>> Handle(GetAllArticlesQuery request, CancellationToken cancellationToken)
    {
        var articles = await _articleCachedRepository.GetAllArticlesAsync();

        if (articles.Count is 0)
            throw new NoEntityException("No articles found");
        
        return articles.Select(x => new Dtos.Article(x)).ToList();
    }
}