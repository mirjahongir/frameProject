using System;
using Core.Results.Normal;

namespace Core.Interfaces.Stages.Normal
{
    public interface ISuccessStage
    {
        ISuccessStage OnNext(Func<Result> result);
        ISuccessStage OnNext(Action<Result> result);
        ISuccessStage OnNext<T>(Func<Result, Tuple<T, Result>> result);
        ISuccessStage OnNext(Action action);
        IErrorStage OnError(Action result);
        IErrorStage OnError(Action<Exception> e);
    }
}
