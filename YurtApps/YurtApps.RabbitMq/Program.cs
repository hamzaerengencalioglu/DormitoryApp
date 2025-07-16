using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YurtApps.Messaging.Consumers;
using YurtApps.Messaging.Contracts.Interfaces;
using YurtApps.Messaging.Services.Mail;


var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((context, services) =>
{
    services.AddScoped<IMailSender, MailSender>();

    services.AddMassTransit(x =>
    {
        x.AddConsumer<MailConsumer>();

        x.UsingRabbitMq((ctx, cfg) =>
        {
            cfg.Host("localhost", "/", h =>
            {
                h.Username("guest");
                h.Password("guest");
            });

            cfg.ReceiveEndpoint("maildto", e =>
            {
                e.ConfigureConsumer<MailConsumer>(ctx);
            });
        });
    });
});

var host = builder.Build();

await host.RunAsync();