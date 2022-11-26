using FaqDomain.Events;

namespace FaqDomain.Aggregates;

/// <summary>
/// Категория
/// </summary>
public class Section : Entity
{
    private string _name;
    private List<Section>? _subsections;
    private List<Article>? _articles;

    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new DomainValidateException("Имя категории не может быть пустым");
            if (value.Length > 40)
                throw new DomainValidateException("Имя категории не может быть более 40 символов");

            _name = value;
        }
    }

    /// <summary>
    /// Идентификатор родительской категории
    /// </summary>
    public int? ParentId { get; set; }

    /// <summary>
    /// Родительская категория
    /// </summary>
    public Section? Parent { get; set; }

    /// <summary>
    /// Подкатегории
    /// </summary>
    public List<Section> Subsections
    {
        get => _subsections ?? new List<Section>();
        set => _subsections = value;
    }

    /// <summary>
    /// Статьи
    /// </summary>
    public List<Article> Articles
    {
        get => _articles ?? new List<Article>(); 
        set => _articles = value;
    }

    /// <summary>
    /// ctor
    /// </summary>
    public Section() { }

    /// <summary>
    /// ctor
    /// </summary>
    public Section(string name, int? parentId)
    {
        Name = name;
        ParentId = parentId;
    }

    /// <summary>
    /// Добавляем событие на отправку сообщение при создании категории
    /// </summary>
    public void AddCreationMessage(string message) => AddDomainEvent(new SectionAddedEvent(message));

    /// <summary>
    /// Обновляет категорию
    /// </summary>
    public void Update(string? newName, int? newParentId)
    {
        var hasSubsections = Subsections.Count != 0;
        var hasArticles = Articles.Count != 0;
        var isBecameRoot = newParentId is null;

        if (isBecameRoot & hasArticles)
            throw new DomainValidateException("Корневая категория не может содержать статей");

        if (!isBecameRoot & hasSubsections)
            throw new DomainValidateException("Только корневые категории могут содержать подкатегории");

        Name = newName!;
        ParentId = newParentId;
    }
}