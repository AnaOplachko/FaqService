using FaqService.Application.Models.SharedDto.OutBound;

namespace FaqService.Application.Models;

public class PaginatedSearchArticlesResult : PaginatedResult
{
    public List<ArticleModel>? Articles { get; init; }
}