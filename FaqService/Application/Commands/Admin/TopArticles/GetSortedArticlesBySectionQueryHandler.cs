using FaqDataAccess.Repositories.ArticleRepository;
using FaqService.Application.Exceptions;
using MediatR;

namespace FaqService.Application.Commands.Admin.TopArticles;

/// <summary>
/// Обработчик запроса на получение отсортированного по позиции списка статей
/// </summary>
public class GetSortedArticlesBySectionQueryHandler : IRequestHandler<GetSortedArticlesBySectionQuery, List<Dtos.Article>>
{
    private readonly IArticleRepository _articleRepository;

    /// <summary>
    /// ctor
    /// </summary>
    public GetSortedArticlesBySectionQueryHandler(IArticleRepository articleRepository)
        => _articleRepository = articleRepository;

    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task<List<Dtos.Article>> Handle(GetSortedArticlesBySectionQuery request, CancellationToken cancellationToken)
    {
        var articles = (await _articleRepository.GetAllArticlesAsync())
            .Where(x=>x.ParentId == request.SectionId)
            .OrderBy(x=>x.OrderPosition)
            .ToList();

        if (articles.Count is 0)
            throw new NoEntityException("No articles found");

        return articles.Select(x => new Dtos.Article(x)).ToList();
    }
}