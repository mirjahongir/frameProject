using System;
using System.Threading.Tasks;

using Jh.Core.Interfaces.Stages.Generic;

namespace Jh.Core.Results.Generic
{
    public partial class Result<T> : ITryStage<T>
    {
        public ISuccessStage<T> StartTry(Action<Result<T>> action)
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

        public ISuccessStage<T> StartTry<T1>(Func<Result<T>, T1> method)
        {
            if (IsChecked) return this;
            try
            {
                var model = method.Invoke(this);
                if (model == null) return this;
                SetObject<T1>(model);
                return this;
            }
            catch (Exception ex)
            {
                return ParseError(ex);
            }
        }

        public ISuccessStage<T> StartTry(Action action)
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
        public ISuccessStage<T> StartTry<T1>(Func<Result<T>, Task<T1>> method)
        {
            if (IsChecked) return this;
            try
            {
                var model = method.Invoke(this).Result;
                if (model == null) return this;
                SetObject<T1>(model);
                return this;
            }
            catch (Exception ex)
            {
                return ParseError(ex);
            }
        }


    }

}
