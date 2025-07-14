namespace YurtApps.Messaging.Contracts.Interfaces
{
    public interface IMessagePublisher<T>
    {
        Task PublishAsync(T message);
    }
}