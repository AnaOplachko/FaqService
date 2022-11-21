using FaqDomain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace FaqDataAccess.Repositories.ArticleRepository;

public class ArticleRepository : IArticleRepository
{
    private readonly FaqDbContext _context;
    public IUnitOfWork UnitOfWork => _context;
    
    /// <summary>
    /// ctor
    /// </summary>
    public ArticleRepository(FaqDbContext context) => _context = context;

    /// <summary>
    /// Возвращает все статьи
    /// </summary>
    public async Task<List<Article>> GetAllArticlesAsync()
        => await _context
            .Articles
            .Include(x=>x.Tags)
            .OrderBy(x => x.OrderPosition)
            .ToListAsync();

    /// <summary>
    /// Возвращает статью по идентификатору
    /// </summary>
    public async Task<Article?> GetArticleByIdAsync(int id)
        => await _context
            .Articles
            .Include(x => x.Tags)
            .FirstOrDefaultAsync(x => x.Id == id);

    /// <summary>
    /// Добавляет новую статью в контекст
    /// </summary>
    public void CreateArticle(Article article) => _context.Entry(article).State = EntityState.Added;
    
    /// <summary>
    /// Обновляет состояние статьи в контексте
    /// </summary>
    public void UpdateArticle(Article article) => _context.Entry(article).State = EntityState.Modified;
    
    /// <summary>
    /// Помечает статью на удаление
    /// </summary>
    public void DeleteArticle(Article article) => _context.Entry(article).State = EntityState.Deleted;

    /// <summary>
    /// Поиск запроса в вопросе, ответе статьи, а так же среди тэгов
    /// </summary>
    public async Task<(List<Article> articles, int total)> SearchArticles(int paginationPage, int paginationPageSize, 
        string searchString = null!)
    {
        var articles = _context.Articles.Include(x=>x.Tags).AsQueryable();
        IQueryable<Article> result = null!;
        
        if (!string.IsNullOrEmpty(searchString))
        {
            var resultFromQuestion = articles
                .Where(x => EF.Functions.ILike(x.Question, $"%{searchString}%"));

            var resultFromAnswer = articles
                .Where(x => EF.Functions.ILike(x.Answer, $"%{searchString}%"));
            
            var resultFromTag = articles
                .Where(x => x.Tags!.Any(tag => EF.Functions.ILike(tag.Name, $"%{searchString}%")));

            result = resultFromQuestion
                .Union(resultFromAnswer)
                .Union(resultFromTag)
                .OrderBy(x=>x.OrderPosition);
        }

        var total = await result.CountAsync();
        
        var articlesToShowOnPage = result
            .Skip((paginationPage - 1) * paginationPageSize)
            .Take(paginationPageSize)
            .ToList();

        return (articlesToShowOnPage, total);
    }

    /// <summary>
    /// Обновляет позицию каждой статьи категории после добавления новой статьи с позицией
    /// </summary>
    public async Task UpdatePositionsAfterArticleAdded(int parentId, int orderPosition)
    {
        var articles = await _context
            .Articles
            .Where(x => x.ParentId == parentId)
            .Where(x => x.OrderPosition >= orderPosition)
            .OrderBy(x => x.OrderPosition)
            .ToListAsync();

        var nextPositionIndex = orderPosition + 1;
        
        foreach (var article in articles)
        {
            article.SetOrderPosition(nextPositionIndex++);
            _context.Entry(article).State = EntityState.Modified;
        }
    }
    
    /// <summary>
    /// Обновляет позицию каждой статьи категории после удаления статьи с позицией
    /// </summary>
    public async Task UpdatePositionsAfterArticleDeleted(int id, int parentId)
    {
        var articles = await _context
            .Articles
            .Where(x => x.ParentId == parentId)
            .Where(x => x.Id != id)
            .Where(x => x.OrderPosition != null)
            .OrderBy(x => x.OrderPosition)
            .ToListAsync();
        
        for (int i = 0; i < articles.Count; i++)
        {
            articles[i].SetOrderPosition(i+1);
            _context.Entry(articles[i]).State = EntityState.Modified;
        }
    }

    /// <summary>
    /// Обновляет позицию каждой статьи категории после изменения позиции статьи
    /// Выполняется в случае изменения позиции при обновлении статьи без перемещения в другую категорию
    /// </summary>
    public async Task UpdatePositionsAfterArticleModified(int id, int parentId, int? orderPosition)
    {
        var articles = await _context
            .Articles
            .Where(x=>x.Id != id)
            .Where(x => x.ParentId == parentId)
            .Where(x => x.OrderPosition != null)
            .OrderBy(x => x.OrderPosition)
            .ToListAsync();

        if (orderPosition is null)
        {
            for (int i = 0; i < articles.Count; i++)
            {
                articles[i].SetOrderPosition(i+1);
                _context.Entry(articles[i]).State = EntityState.Modified;
            } 
        }
        else
        {
            var lowPositionArticles = articles.SkipWhile(x => x.OrderPosition < orderPosition);

            var positionIndex = orderPosition + 1;
        
            foreach (var article in lowPositionArticles)
            {
                article.SetOrderPosition((int)positionIndex++!);
                _context.Entry(article).State = EntityState.Modified;
            }
        }
    }
}