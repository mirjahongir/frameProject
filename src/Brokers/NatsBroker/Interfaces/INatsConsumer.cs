
namespace NatsBroker.Interfaces
{
    public interface INatsHandler<in TMessage> : INatsHandler where TMessage : class
    {
        Task Consume(IConsumeContext<TMessage> context);
    }
    public interface INatsHandler
    {

    }
}
