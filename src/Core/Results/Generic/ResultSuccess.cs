
using System;

using Core.Interfaces.Stages.Generic;

namespace Core.Results.Generic
{
    public partial class Result<T> : ISuccessStage<T>
    {
        public ISuccessStage<T> OnNext(Func<T> next)
        {
            throw new NotImplementedException();
        }

        ISuccessStage<T> ISuccessStage<T>.OnNext(Action action)
        {
            throw new NotImplementedException();
        }
    }
}
