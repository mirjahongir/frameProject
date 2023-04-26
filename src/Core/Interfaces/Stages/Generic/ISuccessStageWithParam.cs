using System;
using System.Threading.Tasks;

using Jh.Core.Results.Generic;

namespace Jh.Core.Interfaces.Stages.Generic
{
    public interface ISuccessStageWithParam<T> : ISuccessStage<T>
    {
        ISuccessStageWithParam<T> OnNext<T1>(Action<Result<T>, T1> method);
        ISuccessStageWithParam<T> OnNext<T1>(Func<Result<T>, T1> method);
        ISuccessStageWithParam<T> OnNext<T1>(Func<Result<T>, T1, T1> method);
        ISuccessStageWithParam<T> OnNext<T1>(Func<Result<T>, T1, Task<T1>> method);
        ISuccessStageWithParam<T> OnNext<T1, T2>(Action<Result<T>, T1, T2> method);
        ISuccessStageWithParam<T> OnNext<T1, T2>(Func<Result<T>, T1, T2, Tuple<T1, T2>> method);
        ISuccessStageWithParam<T> OnNext<T1, T2>(Func<Result<T>, T1, T2, Task<Tuple<T1, T2>>> method);
        ISuccessStageWithParam<T> OnNext<T1, T2>(Func<Result<T>, T1, T2, T1> method);
        ISuccessStageWithParam<T> OnNext<T1, T2>(Func<Result<T>, T1, T2, Task<T1>> method);
    }

}
