using System;
using Core.Results.Normal;

namespace Core.Interfaces.Stages.Normal
{
    public interface IFinallyStage
    {
        Result Result { get; }
        Result FinalyResult(Action action);
        Result FinnalyResult(Func<Result> action);

    }
}
