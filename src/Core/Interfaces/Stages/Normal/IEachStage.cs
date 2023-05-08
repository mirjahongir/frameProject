using System;
using Jh.Core.Results.Normal;

namespace Jh.Core.Interfaces.Stages.Normal
{
    public interface IEachStage : ITryStage
    {
        ITryStage OnEach(Action action);
        ITryStage OnEach(Action<Result> action);

    }
}
