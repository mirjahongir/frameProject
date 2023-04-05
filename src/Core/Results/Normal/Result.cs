using System;

using Core.Errors;
using Core.Interfaces.Stages.Normal;

namespace Core.Results.Normal
{

    public partial class Result
    {
        internal Result() { }
        public bool IsSuccess { get; }
        public bool IsFailed => !IsSuccess;
        public static IEachStage Create()
        {
            return new Result();
        }
        internal FrameException Error { get; }
        private object _first { get; set; }
        private object _second { get; set; }

        private T GetFirstModel<T>()
        {
            return (T)_first;
        }
        private T GetSeconModel<T>()
        {
            return (T)_second;
        }

        private void ParseError(Exception ext)
        {

        }
        private bool IsCheck
        {
            get
            {
                EachAction();
                return IsFailed;

            }
        }
    }

}
