using FaqService.Application.Models;
using FaqService.Application.Models.SharedDto;
using MediatR;

namespace FaqService.Application.Commands.Admin.Article.Search;

/// <summary>
/// Поисковый запрос
/// </summary>
public class SearchArticlesQuery : IRequest<PaginatedSearchArticlesResult>
{
    /// <summary>
    /// Пагинация
    /// </summary>
    public Pagination Pagination { get; set; } = null!;

    /// <summary>
    /// Поисковый запрос
    /// </summary>
    public string SearchQuery { get; set; } = null!;
}