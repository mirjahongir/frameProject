using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Jh.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class JhAuthorize<T> : AuthorizeAttribute, IAuthorizationFilter where T : struct
    {
        #region Default Constructor
        public JhAuthorize()
        {
        }
        public JhAuthorize(T permission)
        {
            Permissions = new T[1];
            Permissions[0] = permission;
        }
        public JhAuthorize(params T[] permissions)
        {
            Permissions = permissions;
        }
        public JhAuthorize(T permissions, int position) : this(permissions)
        {
            Position = position;
        }
        public JhAuthorize(int position, params T[] permissions) : this(permissions)
        {
            Position = position;
        }
        public T[]? Permissions { get; set; }
        public int? Position { get; set; }
        #endregion
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var identity = (ClaimsIdentity)context.HttpContext.User.Identity;
            if (Permissions != null)
            {
                foreach (var permission in Permissions)
                {
                    var hasPermission = identity.Claims.Any(claim => string.Equals(claim.Value, permission.ToString(), StringComparison.OrdinalIgnoreCase));
                    if (hasPermission)
                        return;
                }
            }
            if (Position != null)
            {
                var userPosition = identity.Claims.FirstOrDefault(claim => string.Equals(claim.Type.ToString(), "position", StringComparison.OrdinalIgnoreCase))?.Value;

                bool isValid = int.TryParse(userPosition, out int position);
                if (isValid)
                {
                    var isAuthorizedRole = position >= Position;
                    if (isAuthorizedRole)
                        return;
                }
            }

            context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
            return;
        }
    }
}
