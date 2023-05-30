using System;
using System.Threading.Tasks;
using Jh.Core.Results.Normal;

namespace Jh.Core.Interfaces.Stages.Normal
{
    public interface IErrorStage
    {

        Result Finally(Action action);
        Result Finally(Action<Result> action);
        T Finally<T>(Func<Result, T> method);
        T2 Finally<T, T2>(Func<Result, T, T2> method);
        T2 Finally<T, T2>(Func<Result, T, Task<T2>> method);
        Result Finally<T>(Action<Result, T> method);
        T3 Finally<T, T2, T3>(Func<Result, T, T2, T3> method);

    }
}
