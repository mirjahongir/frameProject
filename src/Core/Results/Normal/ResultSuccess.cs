using System;
using System.Threading.Tasks;
using Jh.Core.Errors;
using Jh.Core.Interfaces.Stages.Normal;

namespace Jh.Core.Results.Normal
{

    /// <summary>
    /// ISuccessStage Bu yerda asosiy logika ishlaydi
    /// </summary>
    public partial class Result : ISuccessStage
    {
        public IErrorStage OnError(Action result)
        {
            if (IsCheck) return this;
            try
            {
                result();
                return this;
            }
            catch (Exception ex)
            {
                ParseError(ex);
                return this;
            }
        }
        public IErrorStage OnError(Action<FrameException> e)
        {
            if (IsCheck) return this;
            try
            {
                e(Error);
                return this;
            }
            catch (Exception ext)
            {
                ParseError(ext);
                return this;
            }
        }
        public ISuccessStage OnNext(Action action)
        {
            if (IsCheck) return this;
            try
            {
                action();
                return this;
            }
            catch (Exception ext)
            {
                ParseError(ext);
                return this;
            }
        }
        public ISuccessStage OnNext(Action<Result> action)
        {
            if (IsCheck) return this;
            try
            {
                action(this);
                return this;
            }
            catch (Exception ext)
            {
                ParseError(ext);
                return this;
            }
        }
        public ISuccessStage OnNext<T>(Func<Result, T> method)
        {
            if (IsCheck) return this;
            try
            {
                var model = method(this);
                SetObject<T>(model);
                return this;
            }
            catch (Exception ext)
            {
                ParseError(ext);
                return this;
            }
        }
        public ISuccessStage OnNextAsync<T>(Func<Result, Task<T>> method)
        {
            if (IsCheck) return this;
            try
            {
                var model = method(this).Result;
                SetObject<T>(model);
                return this;
            }
            catch (Exception ext)
            {
                ParseError(ext);
                return this;
            }
        }
        public ISuccessStage OnNext<T, T1>(Func<Result, Tuple<T, T1>> method)
        {
            if (IsCheck) return this;
            try
            {
                var (model, second) = method(this);
                SetObject<T>(model);
                SetObject<T1>(second);
                return this;
            }
            catch (Exception ext)
            {
                ParseError(ext);
                return this;
            }
        }
        public ISuccessStage OnNext<T, T1>(Action<Result, T, T1> method)
        {
            if (IsCheck) return this;
            try
            {
                var getFirstObject = GetObject<T>();
                var secondObject = GetObject<T1>();
                method(this, getFirstObject, secondObject);
                return this;
            }
            catch (Exception ext)
            {
                ParseError(ext);
                return this;
            }


        }
        public ISuccessStage OnNextAsync<T, T1>(Func<Result, Task<Tuple<T, T1>>> method)
        {
            if (IsCheck) return this;
            try
            {
                var (model, second) = method(this).Result;
                SetObject<T>(model);
                SetObject<T1>(second);
                return this;
            }
            catch (Exception ext)
            {
                ParseError(ext);
                return this;
            }
        }
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

        public ISuccessStage OnNext<T>(Func<Result, T, T> method)
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
        public ISuccessStage OnNextAsync<T>(Func<Result, T, Task<T>> method)
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
        public ISuccessStage OnNext<T, T2>(Func<Result, T, Tuple<T, T2>> method)
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
        public ISuccessStage OnNextAsync<T, T2>(Func<Result, T, Task<Tuple<T, T2>>> method)
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
        public ISuccessStage OnNext<T, T1>(Func<Result, T, T1, T> method)
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
        public ISuccessStage OnNextAsync<T, T1>(Func<Result, T, T1, Task<T>> method)
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

        public ISuccessStage OnNext<T, T1>(Func<Result, T, T1, Tuple<T, T1>> method)
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


        public ISuccessStage OnNextAsync<T, T1>(Func<Result, T, T1, Task<Tuple<T, T1>>> method)
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

        public ISuccessStage OnNextAsync<T, T1>(Func<Result, T, Task<T1>> method)
        {
            throw new NotImplementedException();
        }

       

    }

}
