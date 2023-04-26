using Jh.Core.Attributes;

namespace Jh.Core.Models
{
    public interface AuthBaseModel<T> where T : class
    {
        [UserInfo]
        T UserInfo { get; set; }// = (T)Activator.CreateInstance(typeof(T), new object[0]);

    }
}
