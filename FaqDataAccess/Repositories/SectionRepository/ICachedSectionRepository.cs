using FaqDomain.Aggregates;

namespace FaqDataAccess.Repositories.SectionRepository;

public interface ICachedSectionRepository
{
    /// <summary>
    /// Возвращает все категории из кэшированного репозитория
    /// </summary>
    ValueTask<List<Section>> GetAllSectionsAsync();

    /// <summary>
    /// Возвращает категорию по идентификатору из кэшированного репозитория
    /// </summary>
    ValueTask<Section?> GetSectionByIdAsync(int id);
}