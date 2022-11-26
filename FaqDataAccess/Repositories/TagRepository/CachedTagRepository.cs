using FaqDomain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace FaqDataAccess.Repositories.TagRepository;

public class CachedTagRepository : ICachedTagRepository
{
    private readonly FaqDbContext _context;
    private readonly IMemoryCache _memoryCache;
    private readonly int _cacheExpiration;
    private readonly SemaphoreSlim _semaphore;

    /// <summary>
    /// ctor
    /// </summary>
    public CachedTagRepository(FaqDbContext context, int cacheExpiration, IMemoryCache memoryCache)
    {
        _context = context;
        _memoryCache = memoryCache;
        _cacheExpiration = cacheExpiration;
        _semaphore = new SemaphoreSlim(1, 1);
    }

    /// <summary>
    /// Возвращает все тэги
    /// </summary>
    public async ValueTask<List<Tag>> GetAllTagsAsync()
    {
        var cacheKey = "tags";

        if (_memoryCache.TryGetValue<List<Tag>>(cacheKey, out var tags))
            return tags!;

        try
        {
            await _semaphore.WaitAsync();
            
            if (_memoryCache.TryGetValue(cacheKey, out tags))
                return tags!;

            tags = await _context
                .Tags
                .Include(x=>x.Articles)
                .AsSingleQuery()
                .ToListAsync();

            if (_cacheExpiration != 0)
            {
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(_cacheExpiration));

                _memoryCache.Set(cacheKey, tags, cacheEntryOptions);
            }
        }
        finally
        {
            _semaphore.Release();
        }

        return tags;
    }

    /// <summary>
    /// Возврщает тэг по идентификатору
    /// </summary>
    public async ValueTask<Tag?> GetTagByIdAsync(int id) => (await GetAllTagsAsync()).FirstOrDefault(x => x.Id == id);
}