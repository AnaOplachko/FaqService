using FaqService.Application.Dtos;
using FaqService.Application.Dtos.SharedDto;
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