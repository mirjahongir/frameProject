using System;
using Jh.Core.Interfaces.Stages.Normal;

namespace Jh.Core.Results.Normal
{
    /// <summary>
    /// ISuccessStageWithParam 
    /// </summary>
    public partial class Result : ISuccessStageWithParam
    {
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
                method(this, model);
                return this;

            }
            catch (Exception e)
            {
                ParseError(e);
                return this;
            }
        }

        public ISuccessStageWithParam OnNext<T, T2>(Func<Result, T, Tuple<T, T2>> method)
        {
            if (!IsCheck) return this;
            try
            {
                var model = GetFirstModel<T>();
                _second = method(this, model);
                return this;
            }
            catch (Exception ex) { ParseError(ex); return this; }
        }

        public ISuccessStageWithParam OnNext<T, T1>(Func<Result, T, T1, T> method)
        {
            if (IsCheck) return this;
            try
            {
                var firstModel = GetFirstModel<T>();
                var seconModel = GetSeconModel<T1>();
                firstModel = method(this, firstModel, seconModel);
                return this;
            }
            catch (Exception ext)
            {
                ParseError(ext);
                return this;
            }
        }

        public ISuccessStageWithParam OnNext<T, T1>(Func<Result, T, T1, Tuple<T, T1>> method)
        {
            if (IsCheck) return this;
            try
            {
                var first = GetFirstModel<T>();
                var second = GetSeconModel<T1>();
                (first, second) = method.Invoke(this, first, second);
                return this;
            }
            catch (Exception ext)
            {
                ParseError(ext);
                return this;


            }
        }
    }
}
