using System;
using Jh.Core.Results.Normal;

namespace Jh.Core.Interfaces.Stages.Normal
{
    public interface ITryStage
    {
        ISuccessStage StartTry(Action<Result> result);
        ISuccessStage StartTry(Action result);
        ISuccessStage StartTry<T>(Func<Result, T> method);
    }
}
