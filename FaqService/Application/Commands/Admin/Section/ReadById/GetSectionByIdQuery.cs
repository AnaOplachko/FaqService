using FaqService.Application.Dtos;
using MediatR;

namespace FaqService.Application.Commands.Admin.Section.ReadById;

/// <summary>
/// Запрос на получение категории по идентификатору
/// </summary>
public class GetSectionByIdQuery : IRequest<SectionWithSubs>
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; init; }
}