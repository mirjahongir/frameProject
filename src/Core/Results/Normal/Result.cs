using System;
using System.Collections.Generic;
using System.Diagnostics;

using Jh.Core.Errors;
using Jh.Core.Interfaces.Stages.Normal;

namespace Jh.Core.Results.Normal
{

    public partial class Result
    {
        internal Result() => IsSuccess = true;
        public bool IsSuccess { get; private set; }
        public bool IsFailed => !IsSuccess;
        public static IEachStage Create
        {
            get { return new Result(); }

        }
        //public static ISuccessStage OnNext()
        //{

        //}
        public static ITryStage StartCreate
        {
            get { return new Result(); }
        }

        public FrameException? Error { get; private set; }

        private T GetFirstModel<T>()
        {
            var name = typeof(T).GUID.ToString();
            if (privateData.ContainsKey(name))
            {
                return (T)privateData[name];
            }
            return default;
            //return (T)_first;
        }
        private Dictionary<string, object> _data = new Dictionary<string, object>();
        private readonly Dictionary<string, object> privateData = new Dictionary<string, object>();
        private T GetObject<T>()

        {
            var name = nameof(T).ToLower();
            if (privateData.ContainsKey(name))
            {
                return (T)privateData[name];
            }
            return default;

        }
        private void SetObject<T>(T model)
        {

            var name = typeof(T).GUID.ToString();//typeof(T).Name.ToLower();
            if (privateData.ContainsKey(name))
            {
                privateData[name] = model;
                return;
            }
            privateData.Add(name, model);
        }
        public void SetObject(string name, object obj)
        {
            _data ??= new Dictionary<string, object>();
            _data[name] = obj;
        }

        public TValue GetObject<TValue>(string name)
        {
            return (TValue)_data[name];
        }
        private T GetSeconModel<T>()
        {
            var name = typeof(T).GUID.ToString();
            if (privateData.ContainsKey(name))
            {
                return (T)privateData[name];
            }
            return default;
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
