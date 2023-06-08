using Jh.Core.Errors;

namespace Jh.Core.ViewModels
{
    public interface IBaseResult
    {
        bool IsSuccess { get; set; }
        FrameException Error { get; set; }
    }
}
