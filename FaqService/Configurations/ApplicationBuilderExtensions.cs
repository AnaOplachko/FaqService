using FaqDataAccess;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace FaqService.Configurations;

/// <summary>
/// Методы расширения для конфигурации пайплайна
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Выполняет миграцию базы данных
    /// </summary>
    public static void MigrateDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        
        var db = scope.ServiceProvider.GetRequiredService<FaqDbContext>().Database;
        
        db.SetCommandTimeout(TimeSpan.FromMinutes(5));
        db.Migrate();
        
        using var connection = (NpgsqlConnection)db.GetDbConnection();
        connection.Open();
        connection.ReloadTypes();
    }
}