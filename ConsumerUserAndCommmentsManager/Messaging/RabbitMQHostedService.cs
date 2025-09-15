using ConsumerUserAndCommmentsManager.Messaging.Absctracts;
using Microsoft.Extensions.Hosting;

namespace ConsumerUserAndCommmentsManager.Messaging;

public class RabbitMQHostedService : IHostedService, IAsyncDisposable
{
    private RabbitMqService _service;
    public IMessageBusService Service => _service;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _service = (RabbitMqService)await RabbitMqService.CreateAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;

    public async ValueTask DisposeAsync()
    {
        if (_service is not null)
            await _service.DisposeAsync();
    }
}
