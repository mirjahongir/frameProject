using Jh.Core.Errors;

namespace Jh.Core.ViewModels.Commands.Result
{
    public interface IEventResult<T> : IEventResult
    {
        public T Model { get; set; }

    }
    public interface IEventResult
    {
        public bool IsSuccess { get; set; }
        public FrameException Error { get;  set; }

    }
}
