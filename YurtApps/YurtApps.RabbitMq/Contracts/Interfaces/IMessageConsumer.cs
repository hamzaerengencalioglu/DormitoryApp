namespace YurtApps.Messaging.Contracts.Interfaces
{
    public interface IMessageConsumer<T>
    {
        Task StartAsync(Func<T, Task> handleMessage);
    }
}