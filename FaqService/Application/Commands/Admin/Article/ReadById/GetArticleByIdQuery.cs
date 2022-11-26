using MediatR;

namespace FaqService.Application.Commands.Admin.Article.ReadById;

/// <summary>
/// Запрос на получение статьи по идентификатору
/// </summary>
public class GetArticleByIdQuery : IRequest<Dtos.Article>
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; init; }
}