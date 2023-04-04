
using Core.Errors;
using Core.Interfaces.Stages.Normal;

namespace Core.Results.Normal
{

    public partial class Result
    {
        internal Result() { }
        public bool IsSuccess { get; }
        public bool IsFaild => !IsSuccess;
        public static IEachStage Create()
        {
            return new Result();
        }
        internal FrameException Error { get; }
        
    }

}
