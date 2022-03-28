using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Starware.DatingApp.Core.Domains;
using Starware.DatingApp.Persistence;
using System.Text;

namespace Starware.DatingApp.API.Extensions
{
    public static class AddIdentityServicesExtension
    {
        public static void AddIdentityServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddIdentityCore<AppUser>()
              .AddRoles<AppRole>()
              .AddRoleManager<RoleManager<AppRole>>()
              .AddSignInManager<SignInManager<AppUser>>()
              .AddRoleValidator<RoleValidator<AppRole>>()
              .AddEntityFrameworkStores<DatingAppContext>();
            var x = new string[]
            {
                "ahmed","mohamed","Ali"
            };

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                options.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                     {
                         var accessToken = context.Request.Query["access_token"];
                         var path = context.HttpContext.Request.Path;
                         if (!string.IsNullOrEmpty(accessToken) & path.StartsWithSegments("/hubs"))
                         {
                             context.Token = accessToken;
                         }

                         return Task.CompletedTask;
                     }
                };
            });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("newPolicy", policy => {
                    policy.RequireRole("Admin");
                });
            });

        }
    }
}
