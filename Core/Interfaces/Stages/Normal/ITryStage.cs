using System;
using Core.Results.Normal;

namespace Core.Interfaces.Stages.Normal
{
    public interface ITryStage
    {
        ISuccessStage OnNext(Func<Result> result);
        ISuccessStage OnNext(Action<Result> result);
        ISuccessStage OnNext(Action result);
    }
}
