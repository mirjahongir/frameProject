using Jh.Core.Errors;
using Jh.Core.ViewModels.Commands.Result;
using Jh.Web.Models;
using Jh.Web.Startup;

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Jh.Web.WebResults
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class WebResult<T>
        where T : class
    {
        public void CheckEventResponce(T model)
        {

            if (model is IEventResult result)
            {
                IsSuccess = result.IsSuccess;
            }

            if (!IsSuccess)
                SetStatus();


        }
        private static void SetStatus(int statusCode = 400)
        {
            JhAspExtensions.HttpContext.HttpContext.Response.StatusCode = 400;
        }
        #region Default Constructor
        public WebResult(T model)
        {
            CheckEventResponce(model);
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
        public WebResult(ModelStateDictionary modelState)
        {

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
            if (exception is FrameException exception2)
            {
                ParseError(exception2);
                return;
            }
            Errors = new List<ErrorModal>()
            {
new ErrorModal(exception.Message){ }
            };
            SetStatus();
        }
        public void ParseError(FrameException exception)
        {
            SetStatus();
        }
        public List<ErrorModal> Errors { get; set; }
        public static implicit operator WebResult<T>(Exception ext)
        {
            return new WebResult<T>(ext);
        }

    }
}
