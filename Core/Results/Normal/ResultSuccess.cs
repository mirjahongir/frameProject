using System;

using Core.Interfaces.Stages.Normal;

namespace Core.Results.Normal
{
    /// <summary>
    /// ISuccessStage Bu yerda asosiy logika ishlaydi
    /// </summary>
    public partial class Result : ISuccessStage
    {
        public IErrorStage OnError(Action result)
        {
            throw new NotImplementedException();
        }

        public IErrorStage OnError(Action<Exception> e)
        {
            throw new NotImplementedException();
        }

        public ISuccessStage OnNext<T>(Func<Result, Tuple<T, Result>> result)
        {
            throw new NotImplementedException();
        }
    }
}
