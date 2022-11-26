using System.Text.Json.Serialization;
using MediatR;

namespace FaqService.Application.Commands.Admin.Article.SetTags;

/// <summary>
/// Команда на установку администратором тэгов для статьи
/// </summary>
public class SetTagsCommand : IRequest<Dtos.Article>
{
    /// <summary>
    /// Идентификатор статьи
    /// </summary>
    [JsonIgnore]
    public int Id { get; set; }

    /// <summary>
    /// Идентификаторы тэгов
    /// </summary>
    public ICollection<int> Tags { get; init; } = null!;
}