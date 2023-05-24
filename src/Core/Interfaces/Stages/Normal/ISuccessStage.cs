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
        ISuccessStage OnNext<T>(Func<Result, T> result);
        ISuccessStage OnNextAsync<T>(Func<Result, Task<T>> result);
        ISuccessStage OnNext<T, T1>(Func<Result, Tuple<T, T1>> result);
        ISuccessStage OnNextAsync<T, T1>(Func<Result, T, Task<T1>> method);
        ISuccessStage OnNextAsync<T, T1>(Func<Result, Task<Tuple<T, T1>>> result);
        ISuccessStage OnNext<T>(Func<Result, T, T> method);
        ISuccessStage OnNextAsync<T>(Func<Result, T, Task<T>> method);
        ISuccessStage OnNext<T, T2>(Func<Result, T, Tuple<T, T2>> method);
        ISuccessStage OnNextAsync<T, T2>(Func<Result, T, Task<Tuple<T, T2>>> method);
        ISuccessStage OnNext<T, T1>(Func<Result, T, T1, T> method);
        ISuccessStage OnNextAsync<T, T1>(Func<Result, T, T1, Task<T>> method);
        ISuccessStage OnNext<T, T1>(Func<Result, T, T1, Tuple<T, T1>> method);
        ISuccessStage OnNextAsync<T, T1>(Func<Result, T, T1, Task<Tuple<T, T1>>> method);
        #endregion

        #region On Error
        IErrorStage OnError(Action result);
        IErrorStage OnError(Action<FrameException> e);
        #endregion
    }


}
