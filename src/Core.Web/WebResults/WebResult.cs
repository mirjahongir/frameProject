using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Jh.Core.Errors;
using Jh.Web.Models;

namespace Jh.Web.WebResults
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class WebResult<T>
        where T : class
    {
        #region Default Constructor
        public WebResult(T model)
        {
            Result = model;
        }
        public WebResult(object model)
        {

        }
        public WebResult() { }
        public WebResult(WebResult<T> model)
        {
            Result = model.Result;
            this.HttpStatus = model.HttpStatus;
            this.IsSuccess = model.IsSuccess;
            this.Code = model.Code;
        }
        #endregion

        #region Params
        public T Result { get; set; }
        public int HttpStatus { get; set; }
        public bool IsSuccess { get; set; } = true;
        public int Code { get; set; }

        #endregion

        #region Operator
        public static implicit operator WebResult<T>(T model)
        {
            return new WebResult<T>(model);
        }
        public static implicit operator WebResult<T>(Task<T> model)
        {
            return new WebResult<T>(model.Result);
        }
        public static implicit operator WebResult<T>(int code) => new WebResult<T>(code);
        #endregion
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class WebResult<T>
    {

        public WebResult(Exception exception)
        {
            if (exception is FrameException exception1)
            {
                ParseError(exception1);
                return;
            }
            ParseError(exception);

        }
        public WebResult(FrameException exception)
        {
            ParseError(exception);
        }
        public void ParseError(Exception exception)
        {

        }
        public void ParseError(FrameException exception)
        {

        }
        public List<ErrorModal> Errors { get; set; }
        public static implicit operator WebResult<T>(Exception ext)
        {
            return new WebResult<T>(ext);
        }

    }
}
