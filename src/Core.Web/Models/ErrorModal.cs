
using Jh.Core.Errors;

namespace Jh.Web.Models
{
    public class ErrorModal
    {
        public ErrorModal() { }
        public ErrorModal(string message)
        {
            Message = message;
        }
        public ErrorModal(FrameException frame)
        {
            Message= frame.Message;
            Code = frame.Code.GetValueOrDefault();

        }
        public int Code { get; set; }
        public string Message { get; set; }
        public string Link { get; set; }
        public int HttpStatus { get; set; }
        public string UzbText { get; set; }
        public string RusText { get; set; }
        public string EngText { get; set; }
    }
}
