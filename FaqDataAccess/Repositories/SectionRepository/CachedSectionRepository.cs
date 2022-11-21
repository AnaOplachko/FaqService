using FaqDomain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace FaqDataAccess.Repositories.SectionRepository;

public class CachedSectionRepository : ICachedSectionRepository
{
    private readonly FaqDbContext _context;
    private readonly IMemoryCache _memoryCache;
    private readonly int _cacheExpiration;
    private readonly SemaphoreSlim _semaphore;

    /// <summary>
    /// ctor
    /// </summary>
    public CachedSectionRepository(FaqDbContext context, int cacheExpiration, IMemoryCache memoryCache)
    {
        _context = context;
        _cacheExpiration = cacheExpiration;
        _memoryCache = memoryCache;
        _semaphore = new SemaphoreSlim(1, 1);
    }

    /// <summary>
    /// Возвращает все категории из кэшированного репозитория
    /// </summary>
    public async Task<List<Section>> GetAllSectionsAsync()
    {
        var cacheKey = "sections";

        if (_memoryCache.TryGetValue<List<Section>>(cacheKey, out var sections))
            return sections!;

        try
        {
            await _semaphore.WaitAsync();

            if (_memoryCache.TryGetValue(cacheKey, out sections))
                return sections!;

            sections = await _context
                .Sections
                .Include(x=>x.Subsections)
                .Include(x=>x.Articles)
                .AsSingleQuery()
                .ToListAsync();

            if (_cacheExpiration != 0)
            {
                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(_cacheExpiration));
                _memoryCache.Set(cacheKey, sections, cacheEntryOptions);
            }
        }
        finally
        {
            _semaphore.Release();
        }

        return sections;
    }

    /// <summary>
    /// Возвращает категорию по идентификатору из кэшированного репозитория
    /// </summary>
    public async Task<Section?> GetSectionByIdAsync(int id)
        => (await GetAllSectionsAsync()).FirstOrDefault(x => x.Id == id);
}