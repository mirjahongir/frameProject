using System;
using Jh.Core.Results.Normal;

namespace Jh.Core.Interfaces.Stages.Normal
{
    public interface IErrorStage
    {
        //Result Result { get; }
        Result Finally(Action action);
        Result Finally(Action<Result> action);
        T Finally<T>(Func<Result, T> method);
        T2 Finally<T, T2>(Func<Result, T, T2> method);
        T3 Finally<T, T2, T3>(Func<Result, T, T2, T3> method);
    }
}
