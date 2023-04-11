using System;
using Jh.Core.Interfaces.Stages.Normal;

namespace Jh.Core.Results.Normal
{
    /// <summary>
    /// IEachStage  - har bir Method kirishi oldidan OnEachdan tekshirib kuradi
    /// </summary>
    public partial class Result : IEachStage
    {
        private Action eachAction;
        private Action<Result> eachActionOne;

        public ITryStage OnEach(Action action)
        {
            eachAction = action;
            return this;
        }
        public ITryStage OnEach(Action<Result> action)
        {
            eachActionOne = action;
            return this;
        }
        private void EachAction()
        {
            eachAction?.Invoke();
            eachActionOne?.Invoke(this);
        }

    }
}
