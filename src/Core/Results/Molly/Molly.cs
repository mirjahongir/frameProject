using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jh.Core.Results.Molly
{
    public class Molly<T>
        where T : class, IEquatable<T>
    {
        static Dictionary<T, Model> _data;
        public static TResult TempRun<TResult>(T obj, Func<TResult> method, TimeSpan span)
            where TResult : class
        {
            _data ??= new Dictionary<T, Model>();
            if (_data.ContainsKey(obj))
            {
                var exist = _data[obj];
                if (DateTime.Now > exist.Span)
                {
                    return (TResult)exist.Result;
                }
                _data.Remove(obj);
            }
            var result = method.Invoke();
            if (result == null) return null;
            _data.Add(obj, new Model(result, span));
            return result;
        }
        public static TResult TempRun<TResult>(T obj, Func<Task<TResult>> method, TimeSpan span)
          where TResult : class
        {
            _data ??= new Dictionary<T, Model>();
            if (_data.ContainsKey(obj))
            {
                var exist = _data[obj];
                if (DateTime.Now > exist.Span)
                {
                    return (TResult)exist.Result;
                }
                _data.Remove(obj);
            }
            var result = method().Result;
            if (result == null) return null;
            _data.Add(obj, new Model(result, span));
            return result;
        }
        public static TResult TempRun<TResult>(T obj, Func<T, TResult> method, TimeSpan span)
            where TResult : class
        {
            _data ??= new Dictionary<T, Model>();

            if (_data.ContainsKey(obj))
            {
                var exist = _data[obj];
                if (DateTime.Now > exist.Span)
                {
                    return (TResult)exist.Result;
                }
                _data.Remove(obj);
            }
            var result = method.Invoke(obj);
            if (result == null) return null;
            _data.Add(obj, new Model(result, span));
            return result;
        }
        public static async Task<TResult> TempRun<TResult>(T obj, Func<T, Task<TResult>> method, TimeSpan span)
               where TResult : class
        {
            _data ??= new Dictionary<T, Model>();
            if (_data.ContainsKey(obj))
            {
                var exist = _data[obj];
                if (DateTime.Now > exist.Span)
                {
                    return (TResult)exist.Result;
                }
                _data.Remove(obj);
            }
            var result = await method(obj);
            if (result == null) return null;
            _data.Add(obj, new Model(result, span));
            return result;
        }
    }
    internal class Model

    {
        public Model(object model, TimeSpan span)
        {
            Result = model;
            Span = DateTime.Now + span;
        }
        public object Result { get; }
        public DateTime Span { get; }
    }
}
