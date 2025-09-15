namespace ConsumerUserAndCommmentsManager.Messaging.Absctracts;

public interface IMessageBusService
{
    Task Consume<T>(Func<T, Task> handler);
}
