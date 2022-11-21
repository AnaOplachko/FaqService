using FaqService.Application.Models;
using MediatR;

namespace FaqService.Application.Commands.Admin.Article.ReadAll;

/// <summary>
/// Запрос на получение всех статей
/// </summary>
public class GetAllArticlesQuery : IRequest<List<ArticleModel>>
{
}