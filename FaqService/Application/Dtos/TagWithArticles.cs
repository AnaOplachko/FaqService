namespace FaqService.Application.Dtos;

public class TagWithArticles
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название тэга
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Статьи тэга
    /// </summary>
    public List<Article>? Articles { get; set; }

    public TagWithArticles(FaqDomain.Aggregates.Tag tag, List<FaqDomain.Aggregates.Article> articles)
    {
        Id = tag.Id;
        Name = tag.Name;
        Articles = articles.Select(x => new Article(x)).ToList();
    }

    /// <summary>
    /// ctor
    /// </summary>
    public TagWithArticles() { }
}