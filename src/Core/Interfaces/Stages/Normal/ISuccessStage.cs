using System;
using Jh.Core.Errors;
using Jh.Core.Results.Normal;

namespace Jh.Core.Interfaces.Stages.Normal
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
