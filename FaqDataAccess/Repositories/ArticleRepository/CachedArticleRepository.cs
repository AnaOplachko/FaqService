using FaqDomain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace FaqDataAccess.Repositories.ArticleRepository;

public class CachedArticleRepository : ICachedArticleRepository
{
    private readonly FaqDbContext _context;
    private readonly IMemoryCache _memoryCache;
    private readonly int _cacheExpiration;
    private readonly SemaphoreSlim _semaphore;

    /// <summary>
    /// ctor
    /// </summary>
    public CachedArticleRepository(FaqDbContext context, IMemoryCache memoryCache, int cacheExpiration)
    {
        _context = context;
        _memoryCache = memoryCache;
        _cacheExpiration = cacheExpiration;
        _semaphore = new SemaphoreSlim(1,1);
    }

    /// <summary>
    /// Поиск статей 
    /// </summary>
    public async ValueTask<List<Article>> SearchArticlesAsync(string? searchString)
    {
        IEnumerable<Article> articles = await GetAllArticlesAsync();
        List<Article> result = null!;
        
        if (!string.IsNullOrEmpty(searchString))
        {
            var resultFromQuestion = articles
                .Where(x => x.Question.Contains(searchString, StringComparison.OrdinalIgnoreCase));

            var resultFromAnswer = articles
                .Where(x => x.Answer.Contains(searchString, StringComparison.OrdinalIgnoreCase));

            var resultFromTag = articles
                .Where(x => x.Tags!.Any(tag => tag.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)));

            result = resultFromQuestion
                .Union(resultFromAnswer)
                .Union(resultFromTag)
                .OrderBy(x=>x.OrderPosition)
                .ToList();
        }
        
        return result;
    }

    /// <summary>
    /// Возвращает все статьи
    /// </summary>
    public async ValueTask<List<Article>> GetAllArticlesAsync()
    {
        var cacheKey = "articles";

        if (_memoryCache.TryGetValue<List<Article>>(cacheKey, out var articles))
            return articles!;

        try
        {
            await _semaphore.WaitAsync();
            
            if (_memoryCache.TryGetValue(cacheKey, out articles))
                return articles!;

            articles = await _context
                .Articles
                .Include(article => article.Tags)
                .AsSingleQuery()
                .ToListAsync();

            if (_cacheExpiration != 0)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(_cacheExpiration));

                _memoryCache.Set(cacheKey, articles, cacheEntryOptions);
            }
        }
        finally
        {
            _semaphore.Release();
        }

        return articles;
    }

    /// <summary>
    /// Возвращает статью по идентификатору
    /// </summary>
    public async ValueTask<Article?> GetArticleByIdAsync(int id)
        => (await GetAllArticlesAsync()).FirstOrDefault(x => x.Id == id);
}