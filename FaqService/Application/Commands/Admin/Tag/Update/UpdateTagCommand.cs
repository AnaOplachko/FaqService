using System.Text.Json.Serialization;
using FaqService.Application.Models;
using MediatR;

namespace FaqService.Application.Commands.Admin.Tag.Update;

/// <summary>
/// Команда на изменение тэга
/// </summary>
public class UpdateTagCommand : IRequest<TagModel>
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [JsonIgnore]
    public int Id { get; set; }

    /// <summary>
    /// Название тэга
    /// </summary>
    public string Name { get; init; } = null!;
}