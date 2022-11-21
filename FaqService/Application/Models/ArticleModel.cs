namespace FaqService.Application.Models;

public class ArticleModel
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
    public IReadOnlyCollection<TagModel>? Tags { get; set; } = new List<TagModel>();

    /// <summary>
    /// ctor
    /// </summary>
    public ArticleModel(FaqDomain.Aggregates.Article article)
    {
        Id = article.Id;
        Question = article.Question;
        Answer = article.Answer;
        ParentId = article.ParentId;
        OrderPosition = article.OrderPosition;
        SetTags(article.Tags!);
    }

    /// <summary>
    /// ctor
    /// </summary>
    public ArticleModel() {}
    
    /// <summary>
    /// Установить тэги
    /// </summary>
    private void SetTags(IReadOnlyCollection<FaqDomain.Aggregates.Tag> tags)
    {
        if (tags != null!)
            Tags = tags.Select(x => new TagModel(x)).ToList();
    }
}