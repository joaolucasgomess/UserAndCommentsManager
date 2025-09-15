using System.Text;
using System.Text.Json; 
using ConsumerUserAndCommmentsManager.Messaging.Absctracts;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ConsumerUserAndCommmentsManager.Messaging;

public class RabbitMqService : IMessageBusService, IAsyncDisposable
{
    private readonly IConnection _connection;
    private readonly IChannel _channel;
    private const string ExchangeName = "notify-new-user-service";
    private const string QueueName = "notify-new-user-service-queue";

    private RabbitMqService(IConnection connection, IChannel channel)
    {
        _connection = connection;
        _channel = channel;
    }
    
    public static async Task<IMessageBusService> CreateAsync()
    {
        var factory = new ConnectionFactory { HostName = "localhost"};
        var connection = await factory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();
        
        await channel.QueueDeclareAsync(QueueName,true, false, false);
        await channel.QueueBindAsync(QueueName, ExchangeName, "new-user-event");

        return new RabbitMqService(connection, channel);
    }
    
    public async Task Consume<T>(Func<T, Task> handler)
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (ch, ea) =>
        {
            var message = Encoding.UTF8.GetString(ea.Body.ToArray());
            T @object;
            try
            {
                @object = JsonSerializer.Deserialize<T>(message);            
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var consumerTag = await _channel.BasicConsumeAsync(QueueName, false, consumer);
                throw;
            }
            
            Console.WriteLine($"Received {@object}");
            
            if(@object is not null)
                await handler(@object);
            
            await _channel.BasicAckAsync(ea.DeliveryTag, false);
        };
        
        var consumerTag = await _channel.BasicConsumeAsync(QueueName, false, consumer);
        
        Console.WriteLine($"Consumed {consumerTag}");
    }
    
    public async ValueTask DisposeAsync()
    {
        if(_connection is not null)
            await _connection.DisposeAsync();
        
        if(_channel is not null)
            await _channel.DisposeAsync();
    }
}
