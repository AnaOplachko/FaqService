using FaqDomain.Aggregates;

namespace FaqDataAccess.Repositories.ArticleRepository;

public interface IArticleRepository
{
    IUnitOfWork UnitOfWork { get; }

    /// <summary>
    /// Возвращает список всех статей
    /// </summary>
    Task<List<Article>> GetAllArticlesAsync();

    /// <summary>
    /// Возвращает статью по идентификатору
    /// </summary>
    Task<Article?> GetArticleByIdAsync(int id);
    
    /// <summary>
    /// Создает статью в репозитории
    /// </summary>
    void CreateArticle(Article article);

    /// <summary>
    /// Обновляет статью
    /// </summary>
    void UpdateArticle(Article article);

    /// <summary>
    /// Удаляет статью
    /// </summary>
    void DeleteArticle(Article article);

    /// <summary>
    /// Поиск статей по запросу
    /// </summary>
    Task<(List<Article> articles, int total)> SearchArticles(int paginationPage, int paginationPageSize, string searchString);

    /// <summary>
    /// Обновляет позицию каждой статьи категории после добавления новой статьи с позицией
    /// </summary>
    Task UpdatePositionsAfterArticleAdded(int parentId, int orderPosition);

    /// <summary>
    /// Обновляет позицию каждой статьи категории после удаления статьи с позицией
    /// </summary>
    Task UpdatePositionsAfterArticleDeleted(int id, int parentId);
    
    /// <summary>
    /// Обновляет позицию каждой статьи категории после изменения позиции статьи
    /// </summary>
    Task UpdatePositionsAfterArticleModified(int id, int parentId, int? orderPosition);
}