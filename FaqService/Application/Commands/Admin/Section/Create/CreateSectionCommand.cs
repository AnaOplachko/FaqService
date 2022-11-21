using FaqService.Application.Models;
using MediatR;

namespace FaqService.Application.Commands.Admin.Section.Create;

/// <summary>
/// Команда на создание категории
/// </summary>
public class CreateSectionCommand : IRequest<SectionModel>
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; init; } = null!;

    /// <summary>
    /// Идентификатор родительской категории
    /// </summary>
    public int? ParentId { get; init; }
}