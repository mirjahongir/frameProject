using System;
using Core.Interfaces.Stages.Normal;

namespace Core.Results.Normal
{
    /// <summary>
    /// ITryStage
    /// </summary>
    public partial class Result : ITryStage
    {
        public ISuccessStage StartTry(Func<Result> result)
        {
            throw new NotImplementedException();
        }

        public ISuccessStage StartTry(Action<Result> result)
        {
            throw new NotImplementedException();
        }

        public ISuccessStage StartTry(Action result)
        {
            throw new NotImplementedException();
        }
    }
}
