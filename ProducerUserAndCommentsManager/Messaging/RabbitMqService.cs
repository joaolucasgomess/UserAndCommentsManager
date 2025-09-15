using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using UserAndCommentsManager.Messaging.Abstracts;

namespace UserAndCommentsManager.Messaging;

public class RabbitMqService : IMessageBusService, IAsyncDisposable
{
    private readonly IConnection _connection;
    private readonly IChannel _channel;
    private const string ExchangeName = "notify-new-user-service";

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
        
        await channel.ExchangeDeclareAsync(ExchangeName,  ExchangeType.Direct, true);

        return new RabbitMqService(connection, channel);
    }

    public async Task Publish<T>(T message, string routingKey)
    {
        var payload = JsonSerializer.Serialize(message);
        var byteArray = Encoding.UTF8.GetBytes(payload);
        var props = new BasicProperties();
        
        await _channel.BasicPublishAsync(ExchangeName, routingKey, false, props, byteArray);
    }

    public async ValueTask DisposeAsync()
    {
        if(_connection is not null)
            await _connection.DisposeAsync();
        
        if(_channel is not null)
            await _channel.DisposeAsync();
    }
}
