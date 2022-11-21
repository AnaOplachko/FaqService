namespace FaqDomain.Aggregates;

/// <summary>
/// Тэг
/// </summary>
public class Tag : Entity
{
    /// <summary>
    /// Идентификатор тэга
    /// </summary>
    public int Id { get; private set; }
    
    /// <summary>
    /// Название тэга
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Статьи тэга
    /// </summary>
    public IReadOnlyCollection<Article>? Articles { get; private set; }

    /// <summary>
    /// ctor
    /// </summary>
    public Tag(string name, List<Tag>existingTags) => SetName(name, existingTags);

    /// <summary>
    /// ctor
    /// </summary>
    public Tag() { }

    /// <summary>
    /// Установить название тэга
    /// </summary>
    private void SetName(string name, List<Tag> existingTags)
    {
        if (name.Length >= 30)
            throw new DomainValidateException("Название тэга не может быть более 30 символов");
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainValidateException("Название тэга не может быть пустым");

        var isNonUniqueName = existingTags.Exists(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (isNonUniqueName)
            throw new DomainValidateException("Названия тэгов не могут повторяться");

        Name = name;
    }

    /// <summary>
    /// Обновление тэга
    /// </summary>
    public void Update(string name, List<Tag> existingTags) => SetName(name, existingTags);
}