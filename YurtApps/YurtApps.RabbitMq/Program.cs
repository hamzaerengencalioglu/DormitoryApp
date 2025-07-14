using YurtApps.Messaging.Contracts.Dtos;
using YurtApps.Messaging.Handlers.Mail;
using YurtApps.Messaging.RabbitMq.Connection;
using YurtApps.Messaging.RabbitMq.Consumer;
using YurtApps.Messaging.RabbitMq.Helpers;

var connectionProvider = new RabbitMqConnectionProvider();

await QueueInitializer.DeclareQueueAsync<MailDto>(connectionProvider);

var emailSender = new SmtpMailSender();
var handler = new MailMessageHandler(emailSender);

var consumer = new RabbitMqConsumer<MailDto>(connectionProvider);
await consumer.StartAsync(async message => await handler.HandleAsync(message));

Console.ReadLine();