using FaqService.Application.Dtos;
using FaqService.Application.Dtos.SharedDto;
using MediatR;

namespace FaqService.Application.Commands.User.Article.Search;

/// <summary>
/// Поисковый запрос по статьям
/// </summary>
public class SearchArticlesQuery : IRequest<PaginatedSearchArticlesResult>
{
    /// <summary>
    /// Пагинация
    /// </summary>
    public Pagination? Pagination { get; set; }

    /// <summary>
    /// Поисковый запрос
    /// </summary>
    public string? SearchQuery { get; set; }
}