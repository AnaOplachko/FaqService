using MediatR;

namespace FaqService.Application.Commands.User.Article.ReadById;

/// <summary>
/// Запрос на получение пользователем статьи по идентификатору
/// </summary>
public class GetArticleByIdQuery : IRequest<Dtos.Article>
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; init; }
}