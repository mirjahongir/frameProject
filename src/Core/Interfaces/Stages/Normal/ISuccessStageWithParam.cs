using System;
using System.Threading.Tasks;

using Jh.Core.Results.Normal;

namespace Jh.Core.Interfaces.Stages.Normal
{
    public interface ISuccessStageWithParam : ISuccessStage
    {
        ISuccessStage OnNext<T>(Action<Result, T> method);
        ISuccessStageWithParam OnNext<T>(Func<Result, T, T> method);
        ISuccessStageWithParam OnNext<T>(Func<Result, T, Task<T>> method);
        ISuccessStageWithParam OnNext<T, T2>(Func<Result, T, Tuple<T, T2>> method);
        ISuccessStageWithParam OnNext<T, T2>(Func<Result, T, Task<Tuple<T, T2>>> method);
        ISuccessStageWithParam OnNext<T, T1>(Func<Result, T, T1, T> method);
        ISuccessStageWithParam OnNext<T, T1>(Func<Result, T, T1, Task<T>> method);
        ISuccessStageWithParam OnNext<T, T1>(Func<Result, T, T1, Tuple<T, T1>> method);
        ISuccessStageWithParam OnNext<T, T1>(Func<Result, T, T1, Task<Tuple<T, T1>>> method);

    }
}
