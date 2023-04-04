using System;
using Core.Interfaces.Stages.Normal;

namespace Core.Results.Normal
{
    /// <summary>
    /// IFinallyStage Oxirgi method Hammsidan keyin shu ishlaydi hato Error bergan bulsayam
    /// </summary>
    public partial class Result : IFinallyStage
    {
        Result IFinallyStage.Result { get; }

        public Result FinalyResult(Action action)
        {
            throw new NotImplementedException();
        }

        public Result FinnalyResult(Func<Result> action)
        {
            throw new NotImplementedException();
        }
    }
}
