namespace Jh.Core.ViewModels.Result
{
    public class EventResult<T>
    {
        public bool IsSuccess { get; set; }
        public T Model { get; set; }
    }
}
