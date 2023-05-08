using System;
using Jh.Core.Errors;
using Jh.Core.Results.Generic;

namespace Jh.Core.Interfaces.Stages.Generic
{
    public interface ISuccessStage<T>: IErrorStage<T>
    {
        #region
        ISuccessStage<T> OnNext(Action action);
        ISuccessStage<T> OnNext(Action<Result<T>> action);
        #endregion

        #region Error
        IErrorStage<T> OnError(Action action);
        IErrorStage<T> OnError(Action<Result<T>, FrameException> action);
        #endregion
    }
}
