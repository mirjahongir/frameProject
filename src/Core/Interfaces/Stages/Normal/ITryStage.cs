using System;

using Core.Results.Normal;

namespace Core.Interfaces.Stages.Normal
{
    public interface ITryStage
    {
        ISuccessStage StartTry(Func<Result> result);
        ISuccessStage StartTry(Action<Result> result);
        ISuccessStage StartTry(Action result);
    }
}
