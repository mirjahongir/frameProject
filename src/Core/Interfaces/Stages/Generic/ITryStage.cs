using System;
using System.Threading.Tasks;

using Jh.Core.Results.Generic;

namespace Jh.Core.Interfaces.Stages.Generic
{
    public interface ITryStage<T>
    {
       ISuccessStage<T> StartTry(Action<Result<T>> action);
       ISuccessStage<T> StartTry(Action action);
       ISuccessStage<T> StartTry<T1>(Func<Result<T>, T1> method);
       ISuccessStage<T> StartTry<T1>(Func<Result<T>, Task<T1>> method);
      

    }

}
