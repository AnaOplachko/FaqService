using FaqService.Application.Models;
using MediatR;

namespace FaqService.Application.Commands.User.Section.ReadById;

/// <summary>
/// Запрос на получение пользователем категории по идентификатору
/// </summary>
public class GetSectionByIdQuery : IRequest<SectionWithSubs>
{
    /// <summary>
    /// Идентификатор категории
    /// </summary>
    public int Id { get; init; }
}