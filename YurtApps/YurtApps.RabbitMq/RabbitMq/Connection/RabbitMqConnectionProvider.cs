using RabbitMQ.Client;

namespace YurtApps.Messaging.RabbitMq.Connection
{
    public class RabbitMqConnectionProvider : IConnectionProvider
    {
        public async Task<IConnection> GetConnectionAsync()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            return await factory.CreateConnectionAsync();
        }
    }
}