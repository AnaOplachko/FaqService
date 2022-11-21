using FaqService.Application.Models;
using MediatR;

namespace FaqService.Application.Commands.Admin.Article.ReadById;

/// <summary>
/// Запрос на получение статьи по идентификатору
/// </summary>
public class GetArticleByIdQuery : IRequest<ArticleModel>
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; init; }
}