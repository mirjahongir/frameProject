namespace NatsBroker.Interfaces
{
    public interface IConsumeContext<out T>
        where T : class
    {
        T Message { get; }
    }
    public interface IResponseContext
    {
        ValueTask Respond<T>(T message);
    }
}
