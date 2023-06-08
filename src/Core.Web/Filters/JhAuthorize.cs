using System.Collections;
using System.Reflection;
using System.Security.Claims;

using Jh.Core.Attributes;
using Jh.Web.WebResults;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Newtonsoft.Json;

namespace Jh.Web.Filters
{
    public class ActionValidatorFilter : IAsyncActionFilter, IFilterMetadata
    {
        private static string InterfaceName => "AuthBaseModel";
        private static string UserName => "UserInfo";
        public static async ValueTask NotValid(ActionExecutingContext context)
        {
            WebResult<object> coreResult = new(context.ModelState);
            context.HttpContext.Response.StatusCode = 400;
            context.HttpContext.Response.ContentType = "application/json";
            await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(coreResult));
        }

        public void SearchByProperty(ActionExecutingContext context, object value, Type valueType)
        {
            var list = valueType.GetProperties().Where(m => m.GetCustomAttribute<JwtPropertyAttribute>() != null);
            if (!list.Any()) return;
            foreach (var i in list)
            {
                var attr = i.GetCustomAttribute<JwtPropertyAttribute>();
                if (attr != null)
                    SetJwtValueToProperty(context.HttpContext.User, i, value, attr);
            }
        }
        public void CheckAuthModel(ActionExecutingContext context, object value, Type valueType)
        {
            var exist = valueType.GetInterfaces().FirstOrDefault(m => m.Name.Contains(InterfaceName));
            if (exist == null) return;
            var userInfo = valueType.GetProperties().FirstOrDefault(m => string.Equals(m.Name, UserName));
            var userValue = Activator.CreateInstance(userInfo.PropertyType);
            SetUserInfo(userInfo, context, userValue);
            userInfo.SetValue(value, userValue);
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            foreach (KeyValuePair<string, object> actionArguments in context.ActionArguments)
            {
                var value = actionArguments.Value;
                var valueType = value.GetType();
                SearchByProperty(context, value, valueType);
                CheckAuthModel(context, value, valueType);
            }


            if (!context.ModelState.IsValid)
            {
                await NotValid(context);
                return;
            }
            await next();
        }
        public void SetUserInfo(PropertyInfo userInfo, ActionExecutingContext context, object obj)
        {
            foreach (var i in userInfo.PropertyType.GetProperties())
            {
                var attribute = i.GetCustomAttribute<JwtPropertyAttribute>();
                if (attribute != null)
                    SetJwtValueToProperty(context.HttpContext.User, i, obj, attribute);
            }


        }
        public void SetJwtValueToProperty(ClaimsPrincipal claim, PropertyInfo prop, object value, JwtPropertyAttribute attr)
        {
            object obj = JwtValue(claim, prop, attr.JwtKey);
            if (obj != null || attr.IsRequired)
            {
                prop.SetValue(value, obj);
            }
        }
        public object JwtValue(ClaimsPrincipal claim, PropertyInfo prop, string jwtKey)
        {
            var list = claim.Claims.ToList();
            string text = claim?.Claims?.FirstOrDefault((Claim m) => string.Equals(jwtKey, m.Type, StringComparison.OrdinalIgnoreCase))?.Value;
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            string text2 = prop.PropertyType.Name.ToLower();
            string text3 = text2;
            if (text3 == "list`1")
            {
                List<Claim> claims = claim.Claims.Where((Claim m) => m.Type == jwtKey).ToList();
                return SetClaimsToListProperty(claims, prop);
            }

            return GetValue(prop.PropertyType.Name.ToLower(), text);
        }
        public object GetValue(string typeName, string jwtValue)
        {
            switch (typeName)
            {
                case "string":
                    return jwtValue;
                case "int64":
                    {
                        _ = long.TryParse(jwtValue, out var result2);
                        if (result2 == 0)
                        {
                            return null;
                        }

                        return result2;
                    }
                case "int32":
                    {
                        _ = int.TryParse(jwtValue, out var result3);
                        if (result3 == 0)
                        {
                        }

                        return result3;
                    }
                case "double":
                    {
                        _ = double.TryParse(jwtValue, out var result);
                        if (result == 0.0)
                        {
                        }

                        return result;
                    }
                default:
                    try
                    {
                        return int.Parse(jwtValue);
                    }
                    catch (Exception)
                    {
                        return null;
                    }
            }
        }

        public object SetClaimsToListProperty(List<Claim> claims, PropertyInfo prop)
        {
            string text = prop.PropertyType.GenericTypeArguments[0].Name.ToLower();
            ArrayList arrayList = new ArrayList();
            foreach (Claim claim in claims)
            {
                arrayList.Add(GetValue(text, claim.Value));
            }

            return text switch
            {
                "string" => arrayList.Cast<string>().ToList(),
                "int32" => arrayList.Cast<int>().ToList(),
                "int64" => arrayList.Cast<long>().ToList(),
                "double" => arrayList.Cast<double>().ToList(),
                _ => null,
            };
        }


    }
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
            if (Permissions == null && Position == null)
            {
                return;
            }
            context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
            return;
        }
    }
}
