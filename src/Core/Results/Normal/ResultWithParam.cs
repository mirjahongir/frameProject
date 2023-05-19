using System;
using System.Threading.Tasks;

using Jh.Core.Interfaces.Stages.Normal;

namespace Jh.Core.Results.Normal
{
    /// <summary>
    /// ISuccessStageWithParam 
    /// </summary>
    public partial class Result : ISuccessStageWithParam
    {
        public Result Finally<T>(Action<Result, T> method)
        {
            throw new NotImplementedException();
        }

        public ISuccessStage OnNext<T>(Action<Result, T> method)
        {
            if (IsCheck) return this;
            try
            {
                var model = GetFirstModel<T>();
                method(this, model);
                return this;
            }

            catch (Exception e)
            {
                ParseError(e);
                return this;
            }
        }

        public ISuccessStageWithParam OnNext<T>(Func<Result, T, T> method)
        {
            if (IsCheck) return this;
            try
            {
                var model = GetFirstModel<T>();
                model = method(this, model);
                SetObject(model);
                return this;

            }
            catch (Exception e)
            {
                ParseError(e);
                return this;
            }
        }
        public ISuccessStageWithParam OnNext<T>(Func<Result, T, Task<T>> method)
        {
            if (IsCheck) return this;
            try
            {
                var model = GetFirstModel<T>();
                model = method(this, model).Result;
                SetObject(model);
                return this;

            }
            catch (Exception e)
            {
                ParseError(e);
                return this;
            }
        }
        #region Func<Result, T, Tuple<T, T2>> method
        public ISuccessStageWithParam OnNext<T, T2>(Func<Result, T, Tuple<T, T2>> method)
        {
            if (!IsCheck) return this;
            try
            {
                var model = GetFirstModel<T>();
                var (model1, second) = method(this, model);
                SetObject(model1);
                SetObject(second);
                return this;
            }
            catch (Exception ex) { ParseError(ex); return this; }
        }
        public ISuccessStageWithParam OnNext<T, T2>(Func<Result, T, Task<Tuple<T, T2>>> method)
        {
            if (!IsCheck) return this;
            try
            {
                var model = GetFirstModel<T>();
                var (model1, second) = method(this, model).Result;
                SetObject(model1);
                SetObject(second);
                return this;
            }
            catch (Exception ex) { ParseError(ex); return this; }
        }
        #endregion

        #region Func<Result, T, T1, T> method
        public ISuccessStageWithParam OnNext<T, T1>(Func<Result, T, T1, T> method)
        {
            if (IsCheck) return this;
            try
            {
                var firstModel = GetFirstModel<T>();
                var seconModel = GetSeconModel<T1>();
                firstModel = method(this, firstModel, seconModel);
                SetObject(firstModel);
                return this;
            }
            catch (Exception ext)
            {
                ParseError(ext);
                return this;
            }
        }
        public ISuccessStageWithParam OnNext<T, T1>(Func<Result, T, T1, Task<T>> method)
        {
            if (IsCheck) return this;
            try
            {
                var firstModel = GetFirstModel<T>();
                var seconModel = GetSeconModel<T1>();
                firstModel = method(this, firstModel, seconModel).Result;
                SetObject(firstModel);
                return this;
            }
            catch (Exception ext)
            {
                ParseError(ext);
                return this;
            }
        }
        #endregion

        public ISuccessStageWithParam OnNext<T, T1>(Func<Result, T, T1, Tuple<T, T1>> method)
        {
            if (IsCheck) return this;
            try
            {
                var first = GetFirstModel<T>();
                var second = GetSeconModel<T1>();
                (first, second) = method.Invoke(this, first, second);
                SetObject(first);
                SetObject(second);
                return this;
            }
            catch (Exception ext)
            {
                ParseError(ext);
                return this;
            }
        }


        public ISuccessStageWithParam OnNext<T, T1>(Func<Result, T, T1, Task<Tuple<T, T1>>> method)
        {
            if (IsCheck) return this;
            try
            {
                var first = GetFirstModel<T>();
                var second = GetSeconModel<T1>();
                (first, second) = method.Invoke(this, first, second).Result;
                SetObject(first);
                SetObject(second);
                return this;
            }
            catch (Exception ext)
            {
                ParseError(ext);
                return this;
            }
        }

        public ISuccessStageWithParam OnNext<T, T1>(Func<Result, T, Task<T1>> method)
        {
            throw new NotImplementedException();
        }

        public ISuccessStage OnNext<T, T1>(Task<Action<Result, T, T1>> method)
        {
            throw new NotImplementedException();
        }
    }
}
