using FaqService.Application.Models;
using MediatR;

namespace FaqService.Application.Commands.User.Article.ReadAll;

/// <summary>
/// Запрос на получение пользователем списка всех статей
/// </summary>
public class GetAllArticlesQuery : IRequest<List<ArticleModel>>
{
}