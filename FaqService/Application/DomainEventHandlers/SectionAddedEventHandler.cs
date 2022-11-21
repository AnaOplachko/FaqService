using System.Text;
using FaqDomain.Events;
using MediatR;
using RabbitMQ.Client;

namespace FaqService.Application.DomainEventHandlers;

/// <summary>
/// Обработчик события Section Added Event
/// </summary>
public class SectionAddedEventHandler : INotificationHandler<SectionAddedEvent>
{
    /// <summary>
    /// Обработчик
    /// </summary>
    public Task Handle(SectionAddedEvent notification, CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "LogMessageQueue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(notification.Message);
            
            channel.BasicPublish(exchange:"",
                routingKey:"LogMessageQueue",
                basicProperties:null,
                body: body);
        }

        return Task.CompletedTask;
    }
}