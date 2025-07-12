using YurtApps.RabbitMq;


var sender = new MailSender();
var consumer = new Consumer(sender);
await consumer.Start();
Console.ReadLine();