using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Jh.Core.Errors;
using Jh.Core.Interfaces.Stages.Generic;

namespace Jh.Core.Results.Generic
{
    public partial class Result<T> //: Result
    {
        #region Default Constructor
        private Result()
        {
            IsSuccess = true;
        }
        private Result(T value) : this()
        {
            SetValue(value);
        }
        public bool IsSuccess { get; private set; } = true;
        public bool IsFailed => !IsSuccess;

        private T _value;
        public T Value => _value;
        public void SetError(Exception ext)
        {
            if (ext is FrameException frame)
            {
                Error = frame;
            }
            else
            {
                Error = new FrameException(ext.Message);
            }

        }
        public void SetValue(T value)
        {
            _value = value;
        }

        readonly Dictionary<string, object> objList = new Dictionary<string, object>();

        private T GetObject<T>()
        {
            var tip = typeof(T).GUID.ToString();
            if (objList.ContainsKey(tip))
            {
                return (T)objList[tip];

            }
            return default;

        }

        private void SetObject<T1>(T1 value)
        {
            var tip = typeof(T1).GUID.ToString();
            var exist = objList.FirstOrDefault(m => m.Key == tip);
            if (exist.Key == null)
            {
                objList.Add(tip, value);
            }
            objList[tip] = value;
        }

        private Dictionary<string, object> _data;
        public void SetObject(string name, object obj)
        {
            _data ??= new Dictionary<string, object>();
            _data[name] = obj;
        }
        public object GetObject(string name)
        {
            return _data[name];
        }
        public TValue GetObject<TValue>(string name)
        {
            return (TValue)_data[name];
        }
        private bool IsChecked
        {
            get
            {
                Each();
                return IsFailed;

            }
        }
        private Result<T> ParseError(Exception ext)
        {
            var st = new StackTrace(ext, true);
            var frame = st.GetFrame(0);
            if (ext is FrameException sd)
            {
                Error = sd;

            }
            else
            {
                Error = new FrameException(ext.Message);
            }
            Error.LineNumber = frame.GetFileLineNumber();
            Error.FileName = frame.GetFileName();
            IsSuccess = false;
            return this;
        }

        #endregion

        #region
        public static IEachStage<T> Create(T model)
        {
            return new Result<T>(model);
        }
        public static IEachStage<T> Create()
        {
            return new Result<T>();
        }
        public static ISuccessStage<T> Create(Action<Result<T>> method)
        {
            var result = new Result<T>();
            try
            {
                method.Invoke(result);
                return result;
            }
            catch (Exception ext)
            {
                return null;
            }
        }
        public static ISuccessStage<T> Create(Func<T> method)
        {
            var result = new Result<T>();
            try
            {
                var model = method.Invoke();
                result.SetValue(model);
                return result;
            }
            catch (Exception ext)
            {
                result.ParseError(ext);
            }
            return result;
        }
        public static ISuccessStage<T> Create(Func<Result<T>, T> method)
        {
            var result = new Result<T>();
            try
            {
                var model = method.Invoke(result);
                result.SetValue(model);
                return result;
            }
            catch (Exception ext)
            {
                result.ParseError(ext);

            }
            return result;
        }
        #endregion


        public static explicit operator T(Result<T> result)
        {
            return result.Value;
        }


    }
}
