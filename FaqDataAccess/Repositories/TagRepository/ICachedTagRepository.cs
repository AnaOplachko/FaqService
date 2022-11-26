using FaqDomain.Aggregates;

namespace FaqDataAccess.Repositories.TagRepository;

public interface ICachedTagRepository
{
    /// <summary>
    /// Возвращает все тэги
    /// </summary>
    ValueTask<List<Tag>> GetAllTagsAsync();

    /// <summary>
    /// Возвращает тэг по идентификатору
    /// </summary>
    ValueTask<Tag?> GetTagByIdAsync(int id);
}