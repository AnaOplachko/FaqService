using MediatR;

namespace FaqService.Application.Commands.Admin.Section.Delete;

/// <summary>
/// Команда на удаление категории
/// </summary>
public class DeleteSectionCommand : IRequest
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; init; }
}