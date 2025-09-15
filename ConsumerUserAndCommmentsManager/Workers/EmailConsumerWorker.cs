using ConsumerUserAndCommmentsManager.Events;
using ConsumerUserAndCommmentsManager.Handlers.Abstracts;
using ConsumerUserAndCommmentsManager.Messaging.Absctracts;
using Microsoft.Extensions.Hosting;

namespace ConsumerUserAndCommmentsManager.Workers;

public class EmailConsumerWorker : BackgroundService
{
    private readonly IMessageBusService _busService;
    private readonly IEmailSender _emailSender;

    public EmailConsumerWorker(IMessageBusService busService, IEmailSender emailSender)
    {
        _busService = busService;
        _emailSender = emailSender;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _busService.Consume<NewUserEvent>(async newUser =>
        {
            await _emailSender.SendEmailAsync(newUser.Email, "Novo cadastro", "Bem vindo burro");
        });
    }
}
