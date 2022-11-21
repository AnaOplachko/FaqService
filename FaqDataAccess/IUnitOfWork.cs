using Microsoft.EntityFrameworkCore.Storage;

namespace FaqDataAccess;

public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Сохранить изменения и не триггерить доменные события
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Сохранить изменения и стригеррить доменные события
    /// </summary>
    Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Создать транзакцию
    /// </summary>
    Task<IDbContextTransaction> BeginTransactionAsync();

    /// <summary>
    /// Закоммитить транзакцию
    /// </summary>
    Task CommitTransactionAsync(IDbContextTransaction transaction);

    /// <summary>
    /// Откатить транзакцию
    /// </summary>
    Task RollbackTransactionAsync(IDbContextTransaction transaction);
}