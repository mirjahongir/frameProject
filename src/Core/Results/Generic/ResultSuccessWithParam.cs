using System; 
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

        public ISuccessStageWithParam<T> OnNext<T1, T2>(Func<Result<T>, T1, T2, Tuple<T1, T2>> method)
        {
            if (IsChecked) return this;
            try
            {
                var model = GetObject<T1>();
                var second = GetObject<T2>();
                (model, second) = method(this, model, second);
                return this;
            }
            catch (Exception ex)
            {
                return ParseError(ex);
            }
        }

        public ISuccessStageWithParam<T> OnNext<T1, T2>(Func<Result<T>, T1, T2, T1> method)
        {
            if (IsChecked) return this;
            try
            {
                var model = GetObject<T1>();
                var second = GetObject<T2>();
                model = method(this, model, second);

                return this;
            }
            catch (Exception ex)
            {
                return ParseError(ex);
            }
        }

        public ISuccessStageWithParam<T> OnNext<T1>(Func<Result<T>, T1, T1> method)
        {
            if (IsChecked) return this;
            try
            {
                var model = GetObject<T1>();
                model = method(this, model);
                return this;
            }
            catch (Exception ex)
            {
                return ParseError(ex);
            }
        }
    }
}
