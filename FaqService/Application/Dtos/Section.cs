namespace FaqService.Application.Dtos;

public record Section
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; init; } = null!;

    /// <summary>
    /// Идентификатор родительской категории
    /// </summary>
    public int? ParentId { get; init; }

    /// <summary>
    /// ctor
    /// </summary>
    public Section(FaqDomain.Aggregates.Section section)
    {
        Id = section.Id;
        ParentId = section.ParentId;
        Name = section.Name;
    }
    
    /// <summary>
    /// ctor
    /// </summary>
    public Section() { }
}