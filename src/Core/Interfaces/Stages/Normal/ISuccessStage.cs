using System;

using Core.Errors;
using Core.Interfaces.Stages.Generic;
using Core.Results.Normal;

namespace Core.Interfaces.Stages.Normal
{
    public interface ISuccessStage
    {
        #region OnNext
      
        ISuccessStage OnNext(Action action);
        ISuccessStage OnNext(Action<Result> result);
        ISuccessStageWithParam OnNext<T>(Func<Result, T> result);
        ISuccessStageWithParam OnNext<T, T1>(Func<Result, Tuple<T, T1>> result);
        #endregion

        #region On Error
        IErrorStage OnError(Action result);
        IErrorStage OnError(Action<FrameException> e);
        #endregion
    }
   
    
}
