using System;
using Core.Results.Normal;

namespace Core.Interfaces.Stages.Normal
{
    public interface IEachStage
    {
        ITryStage OnEach(Action action);
        ITryStage OnEach(Action<Result> action);
        ITryStage OnEach(Func<Result, Result> result);
        

    }
}
