using FaqService.Application.Models;
using MediatR;

namespace FaqService.Application.Commands.Admin.Tag.ReadById;

/// <summary>
/// Запрос на получение тэга по идентификатору
/// </summary>
public class GetTagByIdQuery : IRequest<TagWithArticles>
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; init; }
}