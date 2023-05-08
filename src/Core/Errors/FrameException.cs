
namespace Jh.Core.Errors
{
    public class FrameException : System.Exception
    {
        public int? Code { get; }
        public int? Status { get; }
        public int? LineNumber { get; internal set; }
        public string? FileName { get; internal set; }
        public FrameException(int code, string message) : this(message, code)
        {

        }
        public FrameException(string message) : base(message) { }
        public FrameException(string message, int code) : base(message)
        {
            Code = code;
        }
        public FrameException(string message, int code, int status) : this(message, code)
        {
            Status = status;
        }
        public FrameException(string message, int code, string fileName) : this(message, code)
        {
            FileName = fileName;
        }
        public FrameException(string message, int code, int status, string fileName) : this(message, code, status)
        {
            FileName = fileName;
        }
        public FrameException(string message, string fileName, int lineNumber) : base(message)
        {
            FileName = fileName;

        }
        public FrameException(string message, int code, string fileName, int lineNumber) : this(message,
           code, fileName)
        {
            LineNumber = lineNumber;

        }



    }
}
