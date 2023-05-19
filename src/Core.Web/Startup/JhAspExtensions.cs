using System.Text;
using FluentValidation.AspNetCore;
using Jh.Web.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Jh.Web.Startup
{
    public static class JhAspExtensions
    {
        private static IHttpContextAccessor _httpContext;
        internal static IHttpContextAccessor HttpContext
        {
            get
            {
                return _httpContext;
            }
            set
            {
                _httpContext = value;
            }
        }
        internal static void RegisterController(IServiceCollection services)
        {
            services.AddControllers(m =>
            {
                m.Filters.Add(typeof(ActionValidatorFilter));
            })
             .AddNewtonsoftJson(m =>
             {
                 m.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
             })
             .AddFluentValidation();
        }
        public static void DefaultServices(this IServiceCollection services)
        {
            services.AddSession();
            services.AddHttpContextAccessor();
            services.AddCors();
            services.AddDistributedMemoryCache();
            RegisterController(services);
        }
        public static void FullConfigApp(this IApplicationBuilder app)
        {
            HttpContext = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            
          
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();
            app.UseCors(delegate (CorsPolicyBuilder builder)
            {
                builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            });

            app.UseSession();
            app.UseCors(m => m.WithHeaders("x-suggested-filename").WithExposedHeaders());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        public static void AddJwtService(this IServiceCollection services, Action<JwtBearerOptions> action)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidIssuer = AuthConfig.ISSUER,
                    ValidateAudience = false,
                    ValidAudience = AuthConfig.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthConfig.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true

                };
            });
        }
    }
    public static class AuthConfig
    {
        public const string ISSUER = "BillingServer"; // издатель токена
        public const string AUDIENCE = "BillingServerClient"; // потребитель токена
        const string KEY = "c4912430-5418-4ce0-9b75-71fae11f82c1";   // ключ для шифрации
        public const int LIFETIME = 999; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }

    }

}
