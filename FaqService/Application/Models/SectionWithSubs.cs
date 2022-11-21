namespace FaqService.Application.Models;

public class SectionWithSubs
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Идентификатор родительской категории
    /// </summary>
    public int? ParentId { get; set; }

    /// <summary>
    /// Вложенные статьи
    /// </summary>
    public List<ArticleModel> Articles { get; set; } = null!;

    /// <summary>
    /// Вложенные категории
    /// </summary>
    public List<SectionWithSubs> Subsections { get; set; } = null!;

    /// <summary>
    /// ctor
    /// </summary>
    public SectionWithSubs(FaqDomain.Aggregates.Section section)
    {
        Id = section.Id;
        Name = section.Name;
        ParentId = section.ParentId;
        
        Articles = section.Articles != null! 
            ? section.Articles.Select(x=> new ArticleModel(x)).ToList() 
            : new List<ArticleModel>();
        
        Subsections = section.Subsections != null! 
            ? section.Subsections.Select(x=> new SectionWithSubs(x)).ToList() 
            : new List<SectionWithSubs>();
    }

    /// <summary>
    /// ctor
    /// </summary>
    public SectionWithSubs(SectionModel section)
    {
        Id = section.Id;
        Name = section.Name;
        ParentId = section.ParentId;
        Articles = new List<ArticleModel>();
        Subsections = new List<SectionWithSubs>();
    }

    /// <summary>
    /// ctor
    /// </summary>
    public SectionWithSubs() { }
}