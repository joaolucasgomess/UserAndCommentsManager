using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using UserAndCommentsManager.Data;
using UserAndCommentsManager.Messaging;
using UserAndCommentsManager.Messaging.Abstracts;

namespace UserAndCommentsManager.Extentions;

public static class BuilderExtentions
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Producer User Manager", Version = "v1" });
        });
        
        builder.Services.AddDbContext<MinimalContextDb>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
        });

        builder.Services.AddSingleton<RabbitMQHostedService>();
        builder.Services.AddSingleton<IMessageBusService>(sp => 
            sp.GetRequiredService<RabbitMQHostedService>().Service);
        builder.Services.AddHostedService(sp => sp.GetRequiredService<RabbitMQHostedService>());
        
        return builder;
    }
}
