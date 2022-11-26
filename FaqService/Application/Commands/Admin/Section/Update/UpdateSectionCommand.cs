using System.Text.Json.Serialization;
using MediatR;

namespace FaqService.Application.Commands.Admin.Section.Update;

/// <summary>
/// Команда на изменение категории
/// </summary>
public class UpdateSectionCommand : IRequest<Dtos.Section>
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [JsonIgnore]
    public int Id { get; set; }

    /// <summary>
    /// Название категории
    /// </summary>
    public string Name { get; init; } = null!;

    /// <summary>
    /// Идентификатор родительской категории
    /// </summary>
    public int? ParentId { get; init; }
}