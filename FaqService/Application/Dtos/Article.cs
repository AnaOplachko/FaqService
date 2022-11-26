namespace FaqService.Application.Dtos;

public record Article
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Вопрос 
    /// </summary>
    public string Question { get; init; } = null!;

    /// <summary>
    /// Ответ
    /// </summary>
    public string Answer { get; init; } = null!;

    /// <summary>
    /// Идентефикатор родительской категории
    /// </summary>
    public int ParentId { get; init; }

    /// <summary>
    /// Позиция при выводе списка статей на экран
    /// </summary>
    public int? OrderPosition { get; init; }

    /// <summary>
    /// Тэги вопроса
    /// </summary>
    public IReadOnlyCollection<Tag>? Tags { get; set; } = new List<Tag>();

    /// <summary>
    /// ctor
    /// </summary>
    public Article(FaqDomain.Aggregates.Article article)
    {
        Id = article.Id;
        Question = article.Question;
        Answer = article.Answer;
        ParentId = article.ParentId;
        OrderPosition = article.OrderPosition;
        Tags = article.Tags?.Select(x => new Tag(x)).ToList();
    }

    /// <summary>
    /// ctor
    /// </summary>
    public Article() {}
}