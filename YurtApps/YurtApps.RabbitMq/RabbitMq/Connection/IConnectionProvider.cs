using RabbitMQ.Client;

namespace YurtApps.Messaging.RabbitMq.Connection
{
    public interface IConnectionProvider
    {
        Task<IConnection> GetConnectionAsync();
    }
}