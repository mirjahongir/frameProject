using System;
using Core.Results.Normal;

namespace Core.Interfaces.Stages.Normal
{
    public interface IErrorStage
    {
        IFinallyStage Finally(Func<Result> result);
        IFinallyStage Finally(Action result);
    }
}
