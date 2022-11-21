using MediatR;

namespace FaqService.Application.Commands.Admin.Article.Delete;

/// <summary>
/// Команда на удаление статьи
/// </summary>
public class DeleteArticleCommand : IRequest
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; init; }
}