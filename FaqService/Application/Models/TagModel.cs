namespace FaqService.Application.Models;

public class TagModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Название тэга
    /// </summary>
    public string Name { get; init; } = null!;

    /// <summary>
    /// ctor
    /// </summary>
    public TagModel(FaqDomain.Aggregates.Tag tag)
    {
        Id = tag.Id;
        Name = tag.Name;
    }

    /// <summary>
    /// ctor
    /// </summary>
    public TagModel() { }
}