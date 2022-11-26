using FaqDomain.Aggregates;

namespace FaqDataAccess.Repositories.ArticleRepository;

public interface ICachedArticleRepository
{
    /// <summary>
    /// Поиск по статьям в кэшированном репозитории
    /// </summary>
    ValueTask<List<Article>> SearchArticlesAsync(string? searchString);

    /// <summary>
    /// Возвращает все статьи из кэшированного репозитория
    /// </summary>
    ValueTask<List<Article>> GetAllArticlesAsync();
    
    /// <summary>
    /// Возвращает статью по идентификатору из кэшированного репозитория
    /// </summary>
    ValueTask<Article?> GetArticleByIdAsync(int id);
}