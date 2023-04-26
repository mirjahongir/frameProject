using System;

namespace Jh.Core.Attributes
{/// <summary>
/// 
/// </summary>
    public class UserInfoAttribute : Attribute
    {
    }
    /// <summary>
    /// 
    /// </summary>
    public class JwtPropertyAttribute : System.Attribute
    {
        private JwtPropertyAttribute()
        {

        }
        public JwtPropertyAttribute(string jwtKey, bool isRequired = false)
        {
            JwtKey = jwtKey;
            IsRequired = isRequired;
        }
        public string JwtKey { get; set; }
        public bool IsRequired { get; set; }

    }
}
