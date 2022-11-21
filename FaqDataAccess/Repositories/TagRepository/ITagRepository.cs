using FaqDomain.Aggregates;

namespace FaqDataAccess.Repositories.TagRepository;

public interface ITagRepository
{
    IUnitOfWork UnitOfWork { get; }

    /// <summary>
    /// Создать тэг
    /// </summary>
    void CreateTag(Tag tag);

    /// <summary>
    /// Возвращает тэг по идентификатору
    /// </summary>
    Task<Tag?> GetTagByIdAsync(int id);

    /// <summary>
    /// Возвращает все тэги
    /// </summary>
    Task<List<Tag>> GetAllTagsAsync();
    
    /// <summary>
    /// Обновляет тэг
    /// </summary>
    void UpdateTag(Tag tag);

    /// <summary>
    /// Удаляет тэг из репозитория
    /// </summary>
    void DeleteTag(Tag tag);
}