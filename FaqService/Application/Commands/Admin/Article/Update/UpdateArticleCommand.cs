using MediatR;

namespace FaqService.Application.Commands.Admin.Article.Update;

/// <summary>
/// Команда на обновление статьи
/// </summary>
public class UpdateArticleCommand : IRequest<Dtos.Article>
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Вопрос
    /// </summary>
    public string Question { get; init; } = null!;

    /// <summary>
    /// Ответ
    /// </summary>
    public string Answer { get; init; } = null!;

    /// <summary>
    /// Идентификатор родительской категории
    /// </summary>
    public int ParentId { get; init; }

    /// <summary>
    /// Позиция
    /// </summary>
    public int? OrderPosition { get; init; }
}