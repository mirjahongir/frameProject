
namespace Jh.Web.Models
{
    public class ErrorModal
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Link { get; set; }
        public int HttpStatus { get; set; }
        public string UzbText { get; set; }
        public string RusText { get; set; }
        public string EngText { get; set; }
    }
}
