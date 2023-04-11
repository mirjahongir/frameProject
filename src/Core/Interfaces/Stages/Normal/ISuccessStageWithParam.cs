using System;
using Jh.Core.Results.Normal;

namespace Jh.Core.Interfaces.Stages.Normal
{
    public interface ISuccessStageWithParam : ISuccessStage
    {
        ISuccessStage OnNext<T>( Action<Result, T> method);
        ISuccessStageWithParam OnNext<T>(Func<Result, T, T> method);
        ISuccessStageWithParam OnNext<T, T2>(Func<Result, T, Tuple<T, T2>> method);
        ISuccessStageWithParam OnNext<T, T1>(Func<Result, T, T1, T> method);
        ISuccessStageWithParam OnNext<T, T1>(Func<Result, T, T1, Tuple<T, T1>> method);

    }
}
