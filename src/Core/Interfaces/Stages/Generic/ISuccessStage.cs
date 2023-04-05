using System;

namespace Core.Interfaces.Stages.Generic
{
    public interface ISuccessStage<T>
    {
        #region OnNext
        ISuccessStage<T> OnNext(Func<T> next);
        ISuccessStage<T> OnNext(Action action);
        #endregion
        #region Error

        #endregion
    }
}
