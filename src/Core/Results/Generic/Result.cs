using Core.Interfaces.Stages.Generic;
using Core.Results.Normal;

namespace Core.Results.Generic
{
    public partial class Result<T> : Result
    {
        #region Default Constructor
        private Result() { }
        private Result(T value) { }
        private T _value;
        public T Value => _value;
        #endregion

        public IEachStage<T> Create(T model)
        {
            _value = model;
            return this;
        }


    }
}
