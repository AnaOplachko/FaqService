using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FaqDataAccess;

/// <summary>
/// Фабрика для контекста используемая при миграциях
/// </summary>
/// <remarks>
/// environment var "ConnectionStrings__DevelopmentConnection" задаётся для подключения к реальной базе для команд из терминала 
/// export ConnectionStrings__DevelopmentConnection='Server=127.0.0.1;Port=5432;Database=FaqDb;User Id=user;Password=password;'
/// dotnet ef migrations add Initial --project FaqDataAccess
/// </remarks>
// ReSharper disable once UnusedMember.Global / используется при добавлении миграций
public class DbContextDesignFactory : IDesignTimeDbContextFactory<FaqDbContext>
{
    public FaqDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("DevelopmentConnection");
        
        if (string.IsNullOrEmpty(connectionString))
            throw new ArgumentNullException(nameof(connectionString));
                
        var optionsBuilder = new DbContextOptionsBuilder<FaqDbContext>()
            .UseNpgsql(connectionString);

        return new FaqDbContext(optionsBuilder.Options);
    }
}