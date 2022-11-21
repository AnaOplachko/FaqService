using FaqDomain.Aggregates;

namespace FaqDataAccess.Repositories.ArticleRepository;

public interface ICachedArticleRepository
{
    /// <summary>
    /// Поиск по статьям в кэшированном репозитории
    /// </summary>
    Task<List<Article>> SearchArticlesAsync(string? searchString);

    /// <summary>
    /// Возвращает все статьи из кэшированного репозитория
    /// </summary>
    Task<List<Article>> GetAllArticlesAsync();
    
    /// <summary>
    /// Возвращает статью по идентификатору из кэшированного репозитория
    /// </summary>
    Task<Article?> GetArticleByIdAsync(int id);
}