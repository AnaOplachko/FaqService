using FaqDataAccess.Repositories.ArticleRepository;
using FaqService.Application.Exceptions;
using FaqService.Application.Models;
using MediatR;

namespace FaqService.Application.Commands.Admin.Article.Search;

/// <summary>
/// Обработчик поискового запроса по статьям
/// </summary>
public class SearchArticlesQueryHandler : IRequestHandler<SearchArticlesQuery, PaginatedSearchArticlesResult>
{
    private readonly IArticleRepository _articleRepository;

    /// <summary>
    /// ctor
    /// </summary>
    public SearchArticlesQueryHandler(IArticleRepository articleRepository)
        => _articleRepository = articleRepository;

    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task<PaginatedSearchArticlesResult> Handle(SearchArticlesQuery request,
        CancellationToken cancellationToken)
    {
        if (request.SearchQuery is null)
            throw new InvalidSearchRequestException("Поисковый запрос не содержит символов");

        var result = await _articleRepository.SearchArticles(request.Pagination.Page,
            request.Pagination.PageSize, request.SearchQuery);

        return new PaginatedSearchArticlesResult
        {
            Articles = result.articles.Select(x => new ArticleModel(x)).ToList(),
            Pagination = new Models.SharedDto.OutBound.Pagination
            {
                CurrentPage = request.Pagination.Page,
                PageSize = request.Pagination.PageSize,
                TotalCount = result.total
            }
        };
    }
}