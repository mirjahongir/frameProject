using System;
using System.Collections.Generic;
using System.Linq;
using Jh.Core.Interfaces.Stages.Generic;

namespace Jh.Core.Results.Generic
{
    public partial class Result<T> //: Result
    {
        #region Default Constructor
        private Result() { }
        private Result(T value) { }
        public bool IsSuccess { get; private set; }
        public bool IsFailed => !IsSuccess;

        private T _value;
        public T Value => _value;

        public void SetValue(T value)
        {
            _value = value;
        }

        readonly Dictionary<Type, List<object>> objList = new Dictionary<Type, List<object>>();

        private T GetObject<T>()
        {
            var tip = typeof(T);
            if (objList.ContainsKey(tip))
            {
                return GetObjects<T>(objList[tip]);
            }
            return default;

        }
        private T GetObjects<T>(List<object> objectList)
        {
            if (objectList.Count == 1)
            {
                return (T)objectList[0];
            }
            throw new Exception("");


        }
        private void SetObject<T1>(T1 value)
        {
            var tip = typeof(T1);
            var exist = objList.FirstOrDefault(m => m.Key == tip);
            if (exist.Key == null)
            {
                objList.Add(tip, new List<object>() { value });
            }
            else
            {
                exist.Value.Add(value);
            }
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
        #endregion


        public static explicit operator T(Result<T> result)
        {
            return result.Value;
        }

    }
}
