using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Jh.Web.Startup
{
    public static class JhAspExtensions
    {
        public static void DefaultServices(this IServiceCollection services)
        {
            services.AddSession();
            services.AddHttpContextAccessor();
            services.AddCors();
            services.AddDistributedMemoryCache();
        }
        public static void DefaultConfigApp(this IApplicationBuilder app)
        {
            app.UseCors(delegate (CorsPolicyBuilder builder)
            {
                builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            });
            app.UseCookiePolicy();
            app.UseSession();
        }
    }
}
