using FaqService.Application.Dtos.SharedDto.OutBound;

namespace FaqService.Application.Dtos;

public class PaginatedSearchArticlesResult : PaginatedResult
{
    public List<Article>? Articles { get; init; }
}