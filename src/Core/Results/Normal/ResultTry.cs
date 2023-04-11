using System;
using Jh.Core.Interfaces.Stages.Normal;

namespace Jh.Core.Results.Normal
{
    /// <summary>
    /// ITryStage
    /// </summary>
    public partial class Result : ITryStage
    {

        public ISuccessStage StartTry(Action<Result> action)
        {
            if (IsCheck) return this;
            try
            {
                action(this);
                return this;
            }
            catch (Exception ex)
            {
                ParseError(ex);
                return this;
            }
        }

        public ISuccessStage StartTry(Action action)
        {
            if (IsCheck) return this;
            try
            {
                action();
                return this;
            }
            catch (Exception ex)
            {
                ParseError(ex);
                return this;
            }
        }

        public ISuccessStage StartTry<T>(Func<Result, T> method)
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
    }
}
