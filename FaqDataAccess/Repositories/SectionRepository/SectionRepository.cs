using FaqDomain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace FaqDataAccess.Repositories.SectionRepository;

public class SectionRepository : ISectionRepository
{
    private readonly FaqDbContext _context;

    /// <summary>
    /// ctor
    /// </summary>
    public SectionRepository(FaqDbContext context) => _context = context;
    
    public IUnitOfWork UnitOfWork => _context;
    
    /// <summary>
    /// Возвращает все категории
    /// </summary>
    public async Task<List<Section>> GetAllSectionsAsync()
        => await _context
            .Sections
            .Include(x=>x.Subsections)
            .Include(x=>x.Articles)
            .ToListAsync();
    
    /// <summary>
    /// Возвращает категорию по идентификатору
    /// </summary>
    public async Task<Section?> GetSectionByIdAsync(int id)
        => await _context
            .Sections
            .Include(x => x.Subsections)
            .Include(x => x.Articles)
            .FirstOrDefaultAsync(x => x.Id == id);

    /// <summary>
    /// Добавляет новую категорию в контекст
    /// </summary>
    public void CreateSection(Section section) => _context.Entry(section).State = EntityState.Added;

    /// <summary>
    /// Обновляет категорию в контексте
    /// </summary>
    public void UpdateSection(Section updatedSection) => _context.Entry(updatedSection).State = EntityState.Modified;

    /// <summary>
    /// Удаляет категорию из контекста
    /// </summary>
    public void DeleteSection(Section section)
    {
        foreach (var sub in section.Subsections)
        {
            _context.Entry(sub).State = EntityState.Deleted;

            foreach (var article in sub.Articles)
            {
                _context.Entry(article).State = EntityState.Deleted;
            }
        }

        foreach (var article in section.Articles)
            _context.Entry(article).State = EntityState.Deleted;
        
        _context.Entry(section).State = EntityState.Deleted;
    }
}