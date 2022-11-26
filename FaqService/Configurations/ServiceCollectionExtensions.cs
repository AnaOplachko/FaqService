using FaqDataAccess;
using FaqDataAccess.Repositories.ArticleRepository;
using FaqDataAccess.Repositories.SectionRepository;
using FaqDataAccess.Repositories.TagRepository;
using FaqDomain;
using FaqService.Application.Exceptions;
using FaqService.Configurations.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Npgsql;
using Hellang.Middleware.ProblemDetails;

namespace FaqService.Configurations;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Установка настроек соединения с базой данных и кэширования
    /// </summary>
    public static void ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DbSettings>(configuration.GetSection("DBSETTINGS"));
        services.Configure<MemoryCacheSettings>(configuration.GetSection("MemoryCacheSettings"));
    }

    /// <summary>
    /// Конфигурация БД
    /// </summary>
    public static void ConfigureDatabase(this IServiceCollection services)
    {
        services.AddDbContext<FaqDbContext>((provider, options) =>
            {
                var settings = provider.GetRequiredService<IOptions<DbSettings>>().Value;
                options.UseNpgsql(GetConnectionString(settings));
            },
            ServiceLifetime.Scoped,
            ServiceLifetime.Singleton);

        services.AddTransient<IArticleRepository, ArticleRepository>();
        services.AddTransient<ICachedArticleRepository, CachedArticleRepository>(provider =>
        {
            var memoryCacheSettings = provider.GetRequiredService<IOptions<MemoryCacheSettings>>().Value;
            var faqDbContext = provider.GetRequiredService<FaqDbContext>();
            var memoryCache = provider.GetRequiredService<IMemoryCache>();

            return new CachedArticleRepository(faqDbContext, memoryCache, memoryCacheSettings.Expiration);
        });

        services.AddTransient<ISectionRepository, SectionRepository>();
        services.AddTransient<ICachedSectionRepository, CachedSectionRepository>(provider =>
        {
            var memoryCacheSettings = provider.GetRequiredService<IOptions<MemoryCacheSettings>>().Value;
            var faqDbContext = provider.GetRequiredService<FaqDbContext>();
            var memoryCache = provider.GetRequiredService<IMemoryCache>();

            return new CachedSectionRepository(faqDbContext, memoryCacheSettings.Expiration, memoryCache);
        });

        services.AddTransient<ITagRepository, TagRepository>();
        services.AddTransient<ICachedTagRepository, CachedTagRepository>(provider =>
        {
            var memoryCacheSettings = provider.GetRequiredService<IOptions<MemoryCacheSettings>>().Value;
            var faqDbContext = provider.GetRequiredService<FaqDbContext>();
            var memoryCache = provider.GetRequiredService<IMemoryCache>();

            return new CachedTagRepository(faqDbContext, memoryCacheSettings.Expiration, memoryCache);
        });
    }

    /// <summary>
    /// Формирование строки подключения к БД
    /// </summary>
    private static string GetConnectionString(DbSettings settings)
    {
        var connectionStringBuilder = new NpgsqlConnectionStringBuilder
        {
            Host = settings.Host,
            Port = settings.Port,
            Database = settings.DbName,
            Username = settings.User,
            Password = settings.Password,
            NoResetOnClose = true
        };

        return connectionStringBuilder.ConnectionString;
    }

    /// <summary>
    /// Добавляет кастомизированный вывод информации об ошибках
    /// </summary>
    public static void AddCustomProblemDetails(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();

        services.AddProblemDetails(options =>
        {
            //400
            options.Map<DomainValidateException>(ex =>
                new CustomRepresentationProblemDetails(StatusCodes.Status400BadRequest, 
                    ex.Message, serviceProvider));
            options.Map<InvalidSearchRequestException>(ex => 
                new CustomRepresentationProblemDetails(StatusCodes.Status400BadRequest, 
                    ex.Message, serviceProvider));
            
            //404
            options.Map<NoEntityException>(ex =>
                new CustomRepresentationProblemDetails(StatusCodes.Status404NotFound, 
                    ex.Message, serviceProvider));
            
            //503
            options.Map<HttpRequestException>(
                ex => new CustomRepresentationProblemDetails(
                    StatusCodes.Status503ServiceUnavailable, "Service temporary unavailable",
                    serviceProvider));
        });
    }
}