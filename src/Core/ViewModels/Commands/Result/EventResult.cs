
namespace Jh.Core.ViewModels.Commands.Result
{
    public interface ICommandResult<T> : ICommandResult
    {
        public T Model { get; set; }
    }
    
}
