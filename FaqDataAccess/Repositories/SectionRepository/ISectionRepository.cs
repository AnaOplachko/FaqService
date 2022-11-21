using FaqDomain.Aggregates;

namespace FaqDataAccess.Repositories.SectionRepository;

public interface ISectionRepository
{
    IUnitOfWork UnitOfWork { get; }

    /// <summary>
    /// Возвращает лист всех категорий
    /// </summary>
    Task<List<Section>> GetAllSectionsAsync();

    /// <summary>
    /// Возвращает категорию по идентификатору
    /// </summary>
    Task<Section?> GetSectionByIdAsync(int id);

    /// <summary>
    /// Создает категорию в репозитории
    /// </summary>
    void CreateSection(Section section);

    /// <summary>
    /// Обновляет категорию
    /// </summary>
    void UpdateSection(Section updatedSection);

    /// <summary>
    /// Удаляет категорию
    /// </summary>
    void DeleteSection(Section section);
}