﻿using System;
using System.Threading.Tasks;

using Jh.Core.Results.Generic;

namespace Jh.Core.Interfaces.Stages.Generic
{
    public interface ITryStage<T>
    {
        ISuccessStageWithParam<T> StartTry(Action<Result<T>> action);
        ISuccessStageWithParam<T> StartTry(Action action);
        ISuccessStageWithParam<T> StartTry<T1>(Func<Result<T>, T1> method);
        ISuccessStageWithParam<T> StartTry<T1>(Func<Result<T>, Task<T1>> method);



    }

}
