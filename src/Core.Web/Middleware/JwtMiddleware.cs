
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Jh.Web.Middleware
{
    public class JwtAuthentication
    {
        private readonly RequestDelegate _next;

        public JwtAuthentication(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext, JwtBearerHandler handler)
        {

            await _next(httpContext);
        }
    }

}
