using ConsumerUserAndCommmentsManager.Handlers;
using ConsumerUserAndCommmentsManager.Handlers.Abstracts;
using ConsumerUserAndCommmentsManager.Messaging;
using ConsumerUserAndCommmentsManager.Messaging.Absctracts;
using ConsumerUserAndCommmentsManager.Workers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

await Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<IEmailSender, EmailHandlerMock>();
        
        services.AddSingleton<IMessageBusService>(sp => 
            RabbitMqService.CreateAsync().GetAwaiter().GetResult());
        
        services.AddHostedService<EmailConsumerWorker>();
    })
    .RunConsoleAsync();
    