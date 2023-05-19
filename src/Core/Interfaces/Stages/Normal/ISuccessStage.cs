using System;
using System.Threading.Tasks;

using Jh.Core.Errors;
using Jh.Core.Results.Normal;

namespace Jh.Core.Interfaces.Stages.Normal
{
    public interface ISuccessStage : IErrorStage
    {
        #region OnNext

        ISuccessStage OnNext(Action action);
        ISuccessStage OnNext(Action<Result> result);
        ISuccessStage OnNext<T>(Action<Result, T> method);
        ISuccessStage OnNext<T, T1>(Action<Result, T, T1> method);
        ISuccessStage OnNext<T, T1>(Task<Action<Result, T, T1>> method);
        ISuccessStageWithParam OnNext<T>(Func<Result, T> result);
        ISuccessStageWithParam OnNext<T>(Func<Result, Task<T>> result);
        ISuccessStageWithParam OnNext<T, T1>(Func<Result, Tuple<T, T1>> result);
        ISuccessStageWithParam OnNext<T, T1>(Func<Result, T, Task<T1>> method);
        ISuccessStageWithParam OnNext<T, T1>(Func<Result, Task<Tuple<T, T1>>> result);

        #endregion

        #region On Error
        IErrorStage OnError(Action result);
        IErrorStage OnError(Action<FrameException> e);
        #endregion
    }


}
