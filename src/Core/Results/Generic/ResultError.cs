﻿using System;
using System.Threading.Tasks;

using Jh.Core.Interfaces.Stages.Generic;

namespace Jh.Core.Results.Generic
{
    public partial class Result<T> : IErrorStage<T>
    {

        public Result<T> Finally(Action action)
        {
            action();
            return this;
        }

        public Result<T> Finally(Action<Result<T>> action)
        {
            action(this);
            return this;
        }

        public T1 Finally<T1>(Func<Result<T>, T1> method)
        {
            return method(this);
        }
        public T1 Finally<T1>(Func<Result<T>, System.Threading.Tasks.Task<T1>> method)
        {
            return method(this).Result;
        }
        public T2 Finally<T1, T2>(Func<Result<T>, T1, T2> method)
        {
            var model = GetObject<T1>();
            return method(this, model);
        }
        public T2 Finally<T1, T2>(Func<Result<T>, T1, Task<T2>> method)
        {
            var model = GetObject<T1>();
            return method(this, model).Result;
        }

        public T3 Finally<T1, T2, T3>(Func<Result<T>, T1, T2, T3> method)
        {
            var model = GetObject<T1>();
            var second = GetObject<T2>();
            return method(this, model, second);
        }
        public T3 Finally<T1, T2, T3>(Func<Result<T>, T1, T2, System.Threading.Tasks.Task<T3>> method)
        {
            var model = GetObject<T1>();
            var second = GetObject<T2>();
            return method(this, model, second).Result;
        }
        public Result<T> Finally()
        {
            return this;
        }





       
    }
}
