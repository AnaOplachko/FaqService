using Docker.DotNet.Models;
using RabbitMQ.Client;

namespace FaqService.ComponentTests.ExternalEnvironment.Containers;

/// <summary>
/// RabbitMq контейнер
/// </summary>
public class RabbitMqContainer : BaseContainer
{
    /// <summary>
    /// ctor
    /// </summary>
    public RabbitMqContainer()
    {
        Image = "rabbitmq";
        Tag = "3-management-alpine";
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
            
            //Env = new List<string> {"RABBITMQ_DEFAULT_USER=guest", "RABBITMQ_DEFAULT_PASS=guest"},
            
            ExposedPorts = new Dictionary<string, EmptyStruct>
            {
                {
                    "5672", default
                }
            },
            
            HostConfig = new HostConfig
            {
                PortBindings = new Dictionary<string, IList<PortBinding>>
                {
                    {"5672", new List<PortBinding> { new() {HostPort = "5672"}}}
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
                var factory = new ConnectionFactory { HostName = "localhost" };
                using var connection = factory.CreateConnection();
                
                if (!connection.IsOpen) continue;
                
                using var channel = connection.CreateModel();
                channel.QueueDelete("LogMessageQueue");
                //channel.ExchangeDelete();
                
                connection.Close();
                return;
            }
            catch
            {
                //ignore
            }
        }
    }

    /// <summary>
    /// Удаление всех данных из брокера
    /// </summary>
    public void DeleteAllData()
    {
        const string queueName = "LogMessageQueue";
        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        
        if (connection.IsOpen)
        {
            using var channel = connection.CreateModel();
            
            channel.QueueDeclare(queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            
            channel.QueuePurge(queueName);
        }
        
        connection.Close();
    }
}