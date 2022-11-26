using MediatR;

namespace FaqService.Application.Commands.Admin.TopArticles;

/// <summary>
/// Запрос на получение сортированного списка статей по идентификатору категории
/// </summary>
public class GetSortedArticlesBySectionQuery : IRequest<List<Dtos.Article>>
{
    /// <summary>
    /// Идентификатор родительской категории
    /// </summary>
    public int SectionId { get; init; }
}