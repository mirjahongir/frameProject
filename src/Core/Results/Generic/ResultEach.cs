using System;
using Jh.Core.Interfaces.Stages.Generic;

namespace Jh.Core.Results.Generic
{
    public partial class Result<T> : IEachStage<T>
    {
        Action _each;
        Action<Result<T>> _eachModel;
        public ITryStage<T> OnEach(Action action)
        {
            _each = action;
            return this;
        }

        public ITryStage<T> OnEach(Action<Result<T>> action)
        {
            _eachModel = action;
            return this;
        }

        public ITryStage<T> OnEach()
        {
            return this;
        }

        private void Each()
        {
            _each?.Invoke();
            _eachModel?.Invoke(this);
        }
    }
}
