using System;

using Core.Errors;
using Core.Interfaces.Stages.Normal;

using Microsoft.Win32.SafeHandles;

namespace Core.Results.Normal
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
        public ISuccessStageWithParam OnNext<T>(Func<Result, T> method)
        {
            if (IsCheck) return this;
            try
            {
                _first = method(this);
                return this;
            }
            catch (Exception ext)
            {
                ParseError(ext);
                return this;
            }
        }
        public ISuccessStageWithParam OnNext<T, T1>(Func<Result, Tuple<T, T1>> method)
        {
            if (IsCheck) return this;
            try
            {
                (_first, _second) = method(this);
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
