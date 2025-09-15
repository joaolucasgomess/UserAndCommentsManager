namespace UserAndCommentsManager.Messaging.Abstracts;

public interface IMessageBusService
{
    Task Publish<T>(T message, string routingKey);
}
