using System;
using System.Threading.Tasks;

using Jh.Core.Interfaces.Stages.Generic;

namespace Jh.Core.Results.Generic
{
    public partial class Result<T> : ISuccessStageWithParam<T>
    {
        public ISuccessStageWithParam<T> OnNext<T1>(Action<Result<T>, T1> method)
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
        public ISuccessStageWithParam<T> OnNext<T1, T2>(Action<Result<T>, T1, T2> method)
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

        public ISuccessStageWithParam<T> OnNext<T1>(Func<Result<T>, T1> method)
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
        public ISuccessStageWithParam<T> OnNext<T1, T2>(Func<Result<T>, T1, T2, Tuple<T1, T2>> method)
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
        public ISuccessStageWithParam<T> OnNext<T1, T2>(Func<Result<T>, T1, T2, Task<Tuple<T1, T2>>> method)
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
        public ISuccessStageWithParam<T> OnNext<T1, T2>(Func<Result<T>, T1, T2, T1> method)
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
        public ISuccessStageWithParam<T> OnNext<T1, T2>(Func<Result<T>, T1, T2, Task<T1>> method)
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
        #endregion

        #region Func<Result<T>, T1, T1>
        public ISuccessStageWithParam<T> OnNext<T1>(Func<Result<T>, T1, T1> method)
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
        public ISuccessStageWithParam<T> OnNext<T1>(Func<Result<T>, T1, Task<T1>> method)
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

        public ISuccessStageWithParam<T> OnNext<T1, T2>(Func<Result<T>, T1, T2> method)
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

        public ISuccessStageWithParam<T> OnNext<T1, T2>(Func<Result<T>, T1, Task<T2>> method)
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
        #endregion



    }
}
