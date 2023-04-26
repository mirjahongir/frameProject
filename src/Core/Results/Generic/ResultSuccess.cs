using System;

using Jh.Core.Errors;
using Jh.Core.Interfaces.Stages.Generic;


namespace Jh.Core.Results.Generic
{
    public partial class Result<T> : ISuccessStage<T>
    {
        private FrameException Error { get; }
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
        #endregion
    }
}
