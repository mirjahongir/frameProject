using System;
using System.Threading.Tasks;

using Jh.Core.Errors;
using Jh.Core.Results.Generic;

namespace Jh.Core.Interfaces.Stages.Generic
{
    public interface ISuccessStage<T> : IErrorStage<T>
    {
        #region
        ISuccessStage<T> OnNext(Action action);
        ISuccessStage<T> OnNext(Action<Result<T>> action);
        ISuccessStage<T> OnNext<T1>(Action<Result<T>, T1> method);
        ISuccessStage<T> OnNext<T1>(Func<Result<T>, T1> method);
        ISuccessStage<T> OnNext<T1>(Func<Result<T>, T1, T1> method);
        ISuccessStage<T> OnNext<T1, T2>(Action<Result<T>, T1, T2> method);
        ISuccessStage<T> OnNext<T1, T2>(Func<Result<T>, T1, T2, Tuple<T1, T2>> method);

        ISuccessStage<T> OnNextAsync(Func<Result<T>, Task> method);

        
        
        ISuccessStage<T> OnNextAsync<T1>(Func<Result<T>, T1, Task<T1>> method);
        ISuccessStage<T> OnNextAsync<T1>(Func<Result<T>, T1, Task> method);
        
        ISuccessStage<T> OnNextAsync<T1, T2>(Func<Result<T>, T1, T2, Task<Tuple<T1, T2>>> method);
        ISuccessStage<T> OnNextAsync<T1, T2>(Func<Result<T>, T1, T2, Task> method);
        ISuccessStage<T> OnNext<T1, T2>(Func<Result<T>, T1, T2> method);
        ISuccessStage<T> OnNextAsync<T1, T2>(Func<Result<T>, T1, Task<T2>> method);
        ISuccessStage<T> OnNext<T1, T2>(Func<Result<T>, T1, T2, T1> method);
        ISuccessStage<T> OnNextAsync<T1, T2>(Func<Result<T>, T1, T2, Task<T1>> method);

        #endregion

        #region Error
        IErrorStage<T> OnError(Action action);
        IErrorStage<T> OnError(Action<Result<T>, FrameException> action);
        #endregion
    }
}
