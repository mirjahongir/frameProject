using System;
using System.Diagnostics;
using Jh.Core.Errors;
using Jh.Core.Interfaces.Stages.Normal;

namespace Jh.Core.Results.Normal
{

    public partial class Result
    {
        internal Result()
        {
            IsSuccess = true;
        }
        public bool IsSuccess { get; private set; }
        public bool IsFailed => !IsSuccess;
        public static IEachStage Create()
        {
            return new Result();
        }
        internal FrameException? Error { get; private set; }
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
            Error = null;
            if (ext is FrameException)
            {
                Error = (FrameException)ext;
            }
            else
            {
                Error = new FrameException(ext.Message);
            }
            var st = new StackTrace(ext);
            var frame = st.GetFrame(0);
            if (Error.LineNumber == 0)
            {
                Error.LineNumber = frame.GetFileLineNumber();
                Error.FileName = frame.GetFileName();
            }
            IsSuccess = false;

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
