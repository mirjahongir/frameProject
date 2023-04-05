using System;

using Core.Interfaces.Stages.Normal;

namespace Core.Results.Normal
{
    /// <summary>
    /// IErrorStage - Hato chiqqanda shu Methodlar ishlaydi
    /// </summary>
    public partial class Result : IErrorStage
    {
        public IFinallyStage Finally(Func<Result> result)
        {
            throw new NotImplementedException();
        }

        public IFinallyStage Finally(Action result)
        {
            throw new NotImplementedException();
        }
    }
}
