using System;
using Jh.Core.Results.Generic;

namespace Jh.Core.Interfaces.Stages.Generic
{
    public interface IEachStage<T> : ITryStage<T>
    {
        ITryStage<T> OnEach();
        ITryStage<T> OnEach(Action action);
        ITryStage<T> OnEach(Action<Result<T>> action);

    }
}
