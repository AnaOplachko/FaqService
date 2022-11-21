using FaqDomain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace FaqDataAccess.Repositories.TagRepository;

public class TagRepository : ITagRepository
{
    private readonly FaqDbContext _context;
    public IUnitOfWork UnitOfWork => _context;

    /// <summary>
    /// ctor
    /// </summary>
    public TagRepository(FaqDbContext context) => _context = context;

    /// <summary>
    /// Добавляет новый тэг в контекст
    /// </summary>
    public void CreateTag(Tag tag) => _context.Entry(tag).State = EntityState.Added;

    /// <summary>
    /// Возвращает тэг по идентификатору
    /// </summary>
    public async Task<Tag?> GetTagByIdAsync(int id)
        => await _context
            .Tags
            .Include(x => x.Articles)
            .FirstOrDefaultAsync(x => x.Id == id);

    /// <summary>
    /// Возвращает все тэги
    /// </summary>
    public async Task<List<Tag>> GetAllTagsAsync()
        => await _context
            .Tags
            .Include(x=>x.Articles)
            .OrderBy(x => x.Id)
            .ToListAsync();

    /// <summary>
    /// Обновляет состояние тэга в контексте
    /// </summary>
    public void UpdateTag(Tag tag) => _context.Entry(tag).State = EntityState.Modified;

    /// <summary>
    /// Помечает тэг на удаление
    /// </summary>
    public void DeleteTag(Tag tag) => _context.Entry(tag).State = EntityState.Deleted;
}