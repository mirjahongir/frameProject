using System;
using System.Threading.Tasks;

using Jh.Core.Interfaces.Stages.Normal;
using Jh.Core.ViewModels;

namespace Jh.Core.Results.Normal
{
    /// <summary>
    /// IErrorStage - Hato chiqqanda shu Methodlar ishlaydi
    /// </summary>
    public partial class Result : IErrorStage
    {

        public Result Finally(Action<Result> action)
        {
            action(this);
            return this;
        }

        public T Finally<T>(Func<Result, T> method)
        {
            return method(this);
        }

        public T2 Finally<T, T2>(Func<Result, T, T2> method)
        {
            var firsModel = GetFirstModel<T>();

            return method(this, firsModel);
        }
        public T2 Finally<T, T2>(Func<Result, T, Task<T2>> method)
        {
            var firsModel = GetFirstModel<T>();

            return method(this, firsModel).Result;
        }

        public T3 Finally<T, T2, T3>(Func<Result, T, T2, T3> method)
        {
            var firstModel = GetFirstModel<T>();
            var secondModel = GetSeconModel<T2>();
            return method(this, firstModel, secondModel);
        }
        Result IErrorStage.Finally(Action action)
        {
            action();
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
        #endregion

        public T2 FinallyWithResult<T2>(Func<Result, T2, T2> method) where T2 : class
        {
            var result = GetObject<T2>();
            if (result == null || result == default)
            {
                result = Generate<T2>();
            }
            ParseByResultModel(result);
            result = method.Invoke(this, result);
            return result;
        }
    }
}
