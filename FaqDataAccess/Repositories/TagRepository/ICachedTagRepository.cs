using FaqDomain.Aggregates;

namespace FaqDataAccess.Repositories.TagRepository;

public interface ICachedTagRepository
{
    /// <summary>
    /// Возвращает все тэги
    /// </summary>
    Task<List<Tag>> GetAllTagsAsync();

    /// <summary>
    /// Возвращает тэг по идентификатору
    /// </summary>
    Task<Tag?> GetTagByIdAsync(int id);
}