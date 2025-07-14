using YurtApps.Messaging.Contracts.Dtos;
using YurtApps.Messaging.Handlers.Mail;
using YurtApps.Messaging.RabbitMq.Connection;
using YurtApps.Messaging.RabbitMq.Consumer;

var connectionProvider = new RabbitMqConnectionProvider();
var emailSender = new SmtpMailSender();
var handler = new MailMessageHandler(emailSender);

var consumer = new RabbitMqConsumer<MailDto>(connectionProvider);
await consumer.StartAsync(async message => await handler.HandleAsync(message));

Console.WriteLine("MailDto dinleniyor...");
Console.ReadLine();
