using FaqService.Application.Models;
using MediatR;

namespace FaqService.Application.Commands.Admin.Article.Create;

/// <summary>
/// Команда на создание статьи
/// </summary>
public class CreateArticleCommand : IRequest<ArticleModel>
{
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
    /// Позиция статьи 
    /// </summary>
    public int? OrderPosition { get; init; }
}