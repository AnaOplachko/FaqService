using FaqDataAccess.Repositories.ArticleRepository;
using FaqService.Application.Exceptions;
using FaqService.Application.Dtos;
using MediatR;

namespace FaqService.Application.Commands.User.Article.Search;

/// <summary>
/// Обработчик поискового запроса по статьям
/// </summary>
public class SearchArticlesQueryHandler : IRequestHandler<SearchArticlesQuery, PaginatedSearchArticlesResult>
{
    private readonly ICachedArticleRepository _articleCachedRepository;

    /// <summary>
    /// ctor
    /// </summary>
    public SearchArticlesQueryHandler(ICachedArticleRepository articleCachedRepository)
        => _articleCachedRepository = articleCachedRepository;
    
    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task<PaginatedSearchArticlesResult> Handle(SearchArticlesQuery request, 
        CancellationToken cancellationToken)
    {
        if (request.SearchQuery is null)
            throw new InvalidSearchRequestException("Поисковый запрос не может быть пустым");
        
        var articles = await _articleCachedRepository.SearchArticlesAsync(request.SearchQuery);

        var articlesToShowOnPage = articles.Skip((request.Pagination!.Page - 1) * request.Pagination.PageSize)
            .Take(request.Pagination.PageSize).ToList();
        
        return new PaginatedSearchArticlesResult
        {
            Articles = articlesToShowOnPage.Select(x => new Dtos.Article(x)).ToList(),
            Pagination = new Dtos.SharedDto.OutBound.Pagination
            {
                CurrentPage = request.Pagination.Page,
                PageSize = request.Pagination.PageSize,
                TotalCount = articles.Count
            }
        };
    }
}