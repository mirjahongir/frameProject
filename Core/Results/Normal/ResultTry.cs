using System;
using Core.Interfaces.Stages.Normal;

namespace Core.Results.Normal
{
    /// <summary>
    /// ITryStage
    /// </summary>
    public partial class Result : ITryStage
    {
        public ISuccessStage OnNext(Func<Result> result)
        {
            throw new NotImplementedException();
        }

        public ISuccessStage OnNext(Action<Result> result)
        {
            throw new NotImplementedException();
        }

        public ISuccessStage OnNext(Action result)
        {
            throw new NotImplementedException();
        }
    }
}
