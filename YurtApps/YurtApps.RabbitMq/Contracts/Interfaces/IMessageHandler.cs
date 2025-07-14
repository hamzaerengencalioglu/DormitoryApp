namespace YurtApps.Messaging.Contracts.Interfaces
{
    public interface IMessageHandler<T>
    {
        Task HandleAsync(T message);
    }
}