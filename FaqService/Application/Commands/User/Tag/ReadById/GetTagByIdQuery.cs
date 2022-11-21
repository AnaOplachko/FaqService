using FaqService.Application.Models;
using MediatR;

namespace FaqService.Application.Commands.User.Tag.ReadById;

/// <summary>
/// Запрос на получение пользователем тэга по идентификатору
/// </summary>
public class GetTagByIdQuery : IRequest<TagWithArticles>
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; init; }
}