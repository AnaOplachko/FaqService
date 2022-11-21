using FaqService.Application.Models;
using MediatR;

namespace FaqService.Application.Commands.User.Article.ReadById;

/// <summary>
/// Запрос на получение пользователем статьи по идентификатору
/// </summary>
public class GetArticleByIdQuery : IRequest<ArticleModel>
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; init; }
}