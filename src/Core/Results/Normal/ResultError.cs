using System;

using Jh.Core.Interfaces.Stages.Normal;

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
    }
}
