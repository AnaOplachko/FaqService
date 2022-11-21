using FaqService.ComponentTests.ExternalEnvironment.Containers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace FaqService.ComponentTests.ExternalEnvironment;

/// <summary>
/// Класс для работы с внешними зависимостями
/// </summary>
public class ExtEnvironment : IClassFixture<WebApplicationFactory<Program>>
{
    /// <summary>
    /// Контейнер Postgres
    /// </summary>
    private static PostgresContainer? PostgresContainer { get; set; }

    /// <summary>
    /// Контейнер RabbitMq
    /// </summary>
    private static RabbitMqContainer? RabbitMqContainer { get; set; }

    /// <summary>
    /// Тестовый сервер приложения
    /// </summary>
    public static TestServer? TestServer { get; private set; }

    /// <summary>
    /// ctor
    /// </summary>
    public ExtEnvironment()
    {
        PostgresContainer = new PostgresContainer();
        RabbitMqContainer = new RabbitMqContainer();
        
        Task.WaitAll(PostgresContainer.StopAllContainers());
        Task.WaitAll(RabbitMqContainer.StartContainer(), PostgresContainer.StartContainer());

        TestServer = CreateServer();
    }

    /// <summary>
    /// Старт сервера с приложением
    /// </summary>
    private static TestServer CreateServer()
    {
        return new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration(configurationBuilder =>
                {
                    configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
                    configurationBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddEnvironmentVariables();
                });
            }).Server;
    }

    /// <summary>
    /// Очистить состояние
    /// </summary>
    public static void Clean()
    {
        PostgresContainer?.DeleteAllData();
        RabbitMqContainer?.DeleteAllData();
    }
}