using YurtApps.RabbitMq;

var consumer = new Consumer();
await consumer.Start();
Console.ReadLine();