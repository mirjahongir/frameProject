using System;
using System.Threading.Tasks;

using Jh.Core.Results.Generic;

namespace Jh.Core.Interfaces.Stages.Generic
{
    public interface IErrorStage<T>
    {
        Result<T> Finally();
        Result<T> Finally(Action action);

        Result<T> Finally(Action<Result<T>> action);

        T1 FinallyWithResult<T1>(Func<Result<T>, T1, T1> method)
            where T1 : class;

        T2 FinallyWithResult<T1, T2>(Func<Result<T>, T1, T2, T2> method)
            where T2 : class;
        T3 FinallyWithResult<T1, T2, T3>(Func<Result<T>, T1, T2, T3, T3> method) where T3 : class;

        T1 Finally<T1>(Func<Result<T>, T1> method);
        T2 Finally<T1, T2>(Func<Result<T>, T1, T2> method);
        T3 Finally<T1, T2, T3>(Func<Result<T>, T1, T2, T3> method);
        T1 Finally<T1>(Func<Result<T>, Task<T1>> method);
        T2 Finally<T1, T2>(Func<Result<T>, T1, Task<T2>> method);
        T3 Finally<T1, T2, T3>(Func<Result<T>, T1, T2, Task<T3>> method);

    }
}
