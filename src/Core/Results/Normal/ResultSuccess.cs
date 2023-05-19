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
        public ISuccessStageWithParam OnNext<T>(Func<Result, T> method)
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
        public ISuccessStageWithParam OnNext<T>(Func<Result, Task<T>> method)
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
        public ISuccessStageWithParam OnNext<T, T1>(Func<Result, Tuple<T, T1>> method)
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
        public ISuccessStageWithParam OnNext<T, T1>(Func<Result, Task<Tuple<T, T1>>> method)
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


    }

}
