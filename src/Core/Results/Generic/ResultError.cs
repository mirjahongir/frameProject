using System;
using System.Threading.Tasks;

using Jh.Core.Interfaces.Stages.Generic;
using Jh.Core.Results.Normal;
using Jh.Core.ViewModels;
using Jh.Core.ViewModels.Commands.Result;

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

        #region 
        T1 Generate<T1>()
        {
            return Activator.CreateInstance<T1>();
        }
        void ParseByResultModel<T1>(T1 model)
            where T1 : class
        {
            if (model == null || model == default) { return; }
            if (model is IBaseResult cmd)
            {
                cmd.IsSuccess = this.IsSuccess;
                cmd.Error = cmd.Error;
            }

        }
        void ParseByICommandResult<T1>(T1 model)
        {
            

        }
        void ParseByQueryResult<T1>(T1 model)
        {

        }
        public T1 FinallyWithResult<T1>(Func<Result<T>, T1, T1> method) where T1 : class
        {
            var result = GetObject<T1>();
            if (result == null || result == default)
            {
                result = Generate<T1>();
            }
            ParseByResultModel(result);

            return method.Invoke(this, result);
        }

        public T2 FinallyWithResult<T1, T2>(Func<Result<T>, T1, T2, T2> method) where T2 : class
        {
            var result = GetObject<T2>();
            if (result == null || result == default)
            {
                result = Generate<T2>();
            }
            ParseByResultModel(result);
            var firstObject = GetObject<T1>();
            return method.Invoke(this, firstObject, result);
        }

        public T3 FinallyWithResult<T1, T2, T3>(Func<Result<T>, T1, T2, T3, T3> method) where T3 : class
        {
            var result = GetObject<T3>();
            if (result == null || result == default)
            {
                result = Generate<T3>();
            }
            ParseByResultModel(result);
            var firstObject = GetObject<T1>();
            var secondObject = GetObject<T2>();
            return method.Invoke(this, firstObject, secondObject, result);

        }
        #endregion
    }
}
