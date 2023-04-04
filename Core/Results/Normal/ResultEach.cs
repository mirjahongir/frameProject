using System;
using Core.Interfaces.Stages.Normal;

namespace Core.Results.Normal
{
    /// <summary>
    /// IEachStage  - har bir Method kirishi oldidan OnEachdan tekshirib kuradi
    /// </summary>
    public partial class Result : IEachStage
    {
 
     
        public ITryStage OnEach(Action action)
        {
            if (IsFaild) return this;
            return this;
        }

        public ITryStage OnEach(Action<Result> action)
        {
            throw new NotImplementedException();
        }

        public ITryStage OnEach(Func<Result, Result> result)
        {
            throw new NotImplementedException();
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
     

    }
}
