using System;
using Jh.Core.Interfaces.Stages.Generic;

namespace Jh.Core.Results.Generic
{
    public partial class Result<T> : ITryStage<T>
    {
        public ISuccessStageWithParam<T> StartTry(Action<Result<T>> action)
        {
            if (IsChecked) return this;
            try
            {
                action(this);
                return this;
            }
            catch (Exception ex)
            {
                return ParseError(ex);
            }
        }

        public ISuccessStageWithParam<T> StartTry<T1>(Func<Result<T>, T1> method)
        {
            if (IsChecked) return this;
            try
            {
                var model = method.Invoke(this);
                return this;
            }
            catch (Exception ex)
            {
                return ParseError(ex);
            }
        }

        public ISuccessStageWithParam<T> StartTry(Action action)
        {
            if (IsChecked) return this;
            try
            {
                action();
                return this;
            }
            catch (Exception ex)
            {
                return ParseError(ex);
            }
        }
    }

}
