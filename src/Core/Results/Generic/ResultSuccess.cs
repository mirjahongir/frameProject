using System;
using System.Threading.Tasks;
using Jh.Core.Errors;
using Jh.Core.Interfaces.Stages.Generic;


namespace Jh.Core.Results.Generic
{
    public partial class Result<T> : ISuccessStage<T>
    {
        public FrameException Error { get; private set; } = null;
        #region
        public IErrorStage<T> OnError(Action action)
        {
            action();
            return this;
        }

        public IErrorStage<T> OnError(Action<Result<T>, FrameException> action)
        {
            if (this.IsFailed)
                action(this, Error);

            return this;
        }

        #endregion
        #region
        public ISuccessStage<T> OnNext(Action action)
        {
            if (IsChecked) return this;
            try
            {
                action.Invoke();
                return this;
            }
            catch (Exception ex)
            {
                return ParseError(ex);
            }
        }

        public ISuccessStage<T> OnNext(Action<Result<T>> action)
        {

            if (IsChecked) return this;
            try
            {
                action.Invoke(this);
                return this;
            }
            catch (Exception ex)
            {
                return ParseError(ex);
            }
        }

        public ISuccessStage<T> OnNext(Task<Action<Result<T>>> method)
        {
            if (IsChecked) return this;
            try
            {
                var action = method.Result;
                action.Invoke(this);

                return this;
            }
            catch (Exception ex)
            {
                return ParseError(ex);
            }
        }

        public ISuccessStage<T> OnNextAsync(Func<Result<T>, Task> method)
        {
            if (IsChecked) return this;
            try
            {
                method.Invoke(this).Wait();
                return this;
            }
            catch (AggregateException ext)
            {
               var ee= ext.InnerException;
                return ParseError(ee);
            }
        }

        public ISuccessStage<T> OnNextAsync<T1>(Func<Result<T>, T1, Task<T1>> method)
        {
            if (IsChecked) return this;
            try
            {
                var obj = GetObject<T1>();
                obj = method.Invoke(this, obj).Result;
                return this;
            }
            catch (Exception ext)
            {
                return ParseError(ext);
            }
        }

        public ISuccessStage<T> OnNextAsync<T1, T2>(Func<Result<T>, T1, T2, Task<Tuple<T1, T2>>> method)
        {
            if (IsChecked) return this;
            try
            {
                var first = GetObject<T1>();
                var second = GetObject<T2>();
                (first, second) = method(this, first, second).Result;
                return this;
            }
            catch (Exception ext)
            {
                return ParseError(ext);
            }
        }

        public ISuccessStage<T> OnNextAsync<T1, T2>(Func<Result<T>, T1, Task<T2>> method)
        {
            if (IsChecked) return this;
            try
            {
                var first = GetObject<T1>();
                var second = method(this, first).Result;
                SetObject<T2>(second);
                return this;
            }
            catch (Exception ext)
            {
                return ParseError(ext);
            }
        }

        public ISuccessStage<T> OnNextAsync<T1, T2>(Func<Result<T>, T1, T2, Task<T1>> method)
        {
            if (IsChecked) return this;
            try
            {
                var first = GetObject<T1>();
                var second = GetObject<T2>();
                first = method(this, first, second).Result;
                return this;
            }
            catch (Exception ext)
            {
                return ParseError(ext);
            }

        }
        #endregion
        public ISuccessStage<T> OnNext<T1>(Action<Result<T>, T1> method)
        {
            if (IsChecked) return this;
            try
            {
                var model = GetObject<T1>();
                method(this, model);
                return this;
            }
            catch (Exception ex)
            {
                return ParseError(ex);
            }

        }
        public ISuccessStage<T> OnNext<T1, T2>(Action<Result<T>, T1, T2> method)
        {
            if (IsChecked) return this;
            try
            {
                var model = GetObject<T1>();
                var seconModel = GetObject<T2>();
                method(this, model, seconModel);
                return this;
            }
            catch (Exception ex)
            {
                return ParseError(ex);
            }
        }

        public ISuccessStage<T> OnNextq<T1>(Func<Result<T>, T1> method)
        {
            if (IsChecked) return this;
            try
            {
                var model = method(this);
                SetObject<T1>(model);
                return this;
            }
            catch (Exception ex)
            {
                return ParseError(ex);
            }
        }

        #region Func<Result<T>, T1, T2, Tuple<T1, T2>>
        public ISuccessStage<T> OnNext<T1, T2>(Func<Result<T>, T1, T2, Tuple<T1, T2>> method)
        {
            if (IsChecked) return this;
            try
            {
                var model = GetObject<T1>();
                var second = GetObject<T2>();
                (model, second) = method(this, model, second);
                SetObject<T1>(model);
                SetObject<T2>(second);
                return this;
            }
            catch (Exception ex)
            {
                return ParseError(ex);
            }
        }
        public ISuccessStage<T> OneNextAsync<T1, T2>(Func<Result<T>, T1, T2, Task<Tuple<T1, T2>>> method)
        {
            if (IsChecked) return this;
            try
            {
                var model = GetObject<T1>();
                var second = GetObject<T2>();
                (model, second) = method(this, model, second).Result;
                SetObject<T1>(model);
                SetObject<T2>(second);
                return this;
            }
            catch (Exception ex)
            {
                return ParseError(ex);
            }
        }
        #endregion
        #region  Func<Result<T>, T1, T2, T1>
        public ISuccessStage<T> OnNext<T1, T2>(Func<Result<T>, T1, T2, T1> method)
        {
            if (IsChecked) return this;
            try
            {
                var model = GetObject<T1>();
                var second = GetObject<T2>();
                model = method(this, model, second);
                SetObject<T1>(model);
                return this;
            }
            catch (Exception ex)
            {
                return ParseError(ex);
            }
        }
        public ISuccessStage<T> OneNextAsync<T1, T2>(Func<Result<T>, T1, T2, Task<T1>> method)
        {
            if (IsChecked) return this;
            try
            {
                var model = GetObject<T1>();
                var second = GetObject<T2>();
                model = method(this, model, second).Result;
                SetObject<T1>(model);
                return this;
            }
            catch (Exception ex)
            {
                return ParseError(ex);
            }
        }
        public ISuccessStage<T> OnNextAsync<T1, T2>(Func<Result<T>, T1, T2, Task> method)
        {
            if (IsChecked) return this;
            try
            {
                var model = GetObject<T1>();
                var second = GetObject<T2>();
                method(this, model, second).Wait();

                return this;
            }
            catch (Exception ex)
            {
                return ParseError(ex);
            }

        }
        #endregion

        #region Func<Result<T>, T1, T1>
        public ISuccessStage<T> OnNext<T1>(Func<Result<T>, T1, T1> method)
        {
            if (IsChecked) return this;
            try
            {
                var model = GetObject<T1>();
                model = method(this, model);
                SetObject<T1>(model);
                return this;
            }
            catch (Exception ex)
            {
                return ParseError(ex);
            }
        }
        public ISuccessStage<T> OneNextAsync<T1>(Func<Result<T>, T1, Task<T1>> method)
        {
            if (IsChecked) return this;
            try
            {
                var model = GetObject<T1>();
                model = method(this, model).Result;
                SetObject<T1>(model);
                return this;
            }
            catch (Exception ex)
            {
                return ParseError(ex);
            }
        }

        public ISuccessStage<T> OnNext<T1, T2>(Func<Result<T>, T1, T2> method)
        {
            if (IsChecked) return this;
            try
            {
                var model = GetObject<T1>();
                var second = method(this, model);
                SetObject<T2>(second);
                return this;
            }
            catch (Exception ex)
            {
                return ParseError(ex);
            }
        }

        public ISuccessStage<T> OneNextAsync<T1, T2>(Func<Result<T>, T1, Task<T2>> method)
        {
            if (IsChecked) return this;
            try
            {
                var model = GetObject<T1>();
                var second = method(this, model).Result;
                SetObject<T2>(second);
                return this;
            }
            catch (Exception ex)
            {
                return ParseError(ex);
            }
        }

        public ISuccessStage<T> OnNext<T1>(Func<Result<T>, T1> method)
        {
            if (IsChecked) return this;
            try
            {
                var obj = method(this);
                SetObject<T1>(obj);
                return this;
            }
            catch (Exception ext)
            {
                return ParseError(ext);
            }
        }

        public ISuccessStage<T> OnNextAsync<T1>(Func<Result<T>, T1, Task> method)
        {
            if (IsChecked) return this;
            try
            {
                var model = GetObject<T1>();
                method(this, model).Wait();
                SetObject<T1>(model);
                return this;
            }
            catch (Exception ext)
            {
                return ParseError(ext);
            }
        }


        #endregion
    }
}
