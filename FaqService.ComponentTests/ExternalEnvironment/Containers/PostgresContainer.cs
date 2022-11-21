using System.Data;
using Docker.DotNet.Models;
using Npgsql;

namespace FaqService.ComponentTests.ExternalEnvironment.Containers;

/// <summary>
/// Postgres контейнер
/// </summary>
public class PostgresContainer : BaseContainer
{
    /// <summary>
    /// ctor
    /// </summary>
    public PostgresContainer()
    {
        Image = "postgres";
        Tag = "12-alpine";
    }
    
    /// <summary>
    /// Старт контейнера
    /// </summary>
    public override async Task StartContainer()
    {
        await PullImage(Image, Tag);

        var response = await DockerClient.Containers.CreateContainerAsync(new CreateContainerParameters
        {
            Image = ImageFull,
            Env = new List<string> {"POSTGRES_PASSWORD=password", "POSTGRES_USER=user"},
            
            ExposedPorts = new Dictionary<string, EmptyStruct>
            {
                {
                    "5432", default
                }
            },
            
            HostConfig = new HostConfig
            {
                PortBindings = new Dictionary<string, IList<PortBinding>>
                {
                    {"5432", new List<PortBinding> {new() {HostPort = "5432"}}}
                },
                PublishAllPorts = true
            }
        });

        ContainerId = response.ID;

        await DockerClient.Containers.StartContainerAsync(ContainerId, null);
        await WaitContainer();
    }

    /// <summary>
    /// Ожидание освобождения контейнера
    /// </summary>
    protected override async Task WaitContainer()
    {
        for (var i = 0; i < 30; i++)
        {
            await Task.Delay(new TimeSpan(0, 0, 0, 1));

            try
            {
                var connectionString = GetConnectionString(null!);
                await using var connection = new NpgsqlConnection(connectionString);
                
                connection.Open();

                if (connection.State != ConnectionState.Open) continue;
                await connection.CloseAsync();
                return;
            }
            catch
            {
                //ignore
            }
        }
    }

    /// <summary>
    /// Удаление данных из БД
    /// </summary>
    public void DeleteAllData()
    {
        var connectionString = GetConnectionString("FaqDb");
        using var connection = new NpgsqlConnection(connectionString);
        
        connection.Open();

        if (connection.State == ConnectionState.Open)
        {
            var clearData = new NpgsqlCommand(
                @"DELETE FROM public.""Articles""; 
                    DELETE FROM public.""Sections"";
                    DELETE FROM public.""Tags"";
                    DELETE FROM public.""ArticleTag"";", connection);

            clearData.ExecuteNonQuery();
            connection.Close();
        }
    }

    /// <summary>
    /// Возвращает строку подключения к БД
    /// </summary>
    private static string GetConnectionString(string dbName)
    {
        var connectionStringBuilder = new NpgsqlConnectionStringBuilder
        {
            Host = "localhost",
            Port = 5432,
            Database = dbName,
            Username = "user",
            Password = "password"
        };

        return connectionStringBuilder.ConnectionString;
    }
}