using System.Data;
using FaqDataAccess.ModelConfigurations;
using FaqDomain.Aggregates;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FaqDataAccess;

public class FaqDbContext : DbContext, IUnitOfWork
{
    /// <summary>
    /// Статьи
    /// </summary>
    public DbSet<Article> Articles { get; set; } = null!;

    /// <summary>
    /// Категории
    /// </summary>
    public DbSet<Section> Sections { get; set; } = null!;

    /// <summary>
    /// Тэги
    /// </summary>
    public DbSet<Tag> Tags { get; set; } = null!;

    private readonly IMediator _mediator = null!;

    /// <summary>
    /// ctor
    /// </summary>
    public FaqDbContext(DbContextOptions<FaqDbContext> options) : base(options) { }

    /// <summary>
    /// ctor
    /// </summary>
    public FaqDbContext(DbContextOptions<FaqDbContext> options, IMediator mediator) : base(options)
        => _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    
    /// <summary>
    /// Конфигурация моделей
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ArticleConfiguration());
        modelBuilder.ApplyConfiguration(new SectionConfiguration());
        modelBuilder.ApplyConfiguration(new TagConfiguration());
    }

    /// <summary>
    /// Сохраняет изменения контекста в базу данных
    /// </summary>
    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await using var transaction = await BeginTransactionAsync();

        try
        {
            await _mediator.DispatchDomainEventsAsync(this);
            await SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }

        return true;
    }

    /// <summary>
    /// Начинает транзакцию
    /// </summary>
    public Task<IDbContextTransaction> BeginTransactionAsync() 
        => Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
    
    /// <summary>
    /// Коммитит транзакцию
    /// </summary>
    public Task CommitTransactionAsync(IDbContextTransaction transaction) => transaction.CommitAsync();

    /// <summary>
    /// Откатывает транзакцию
    /// </summary>
    public Task RollbackTransactionAsync(IDbContextTransaction transaction) => transaction.RollbackAsync();
}