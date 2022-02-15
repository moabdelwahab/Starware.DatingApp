using Microsoft.EntityFrameworkCore;
using Starware.DatingApp.Application.Services;
using Starware.DatingApp.Core.InfrastructureContracts;
using Starware.DatingApp.Core.PersistenceContracts;
using Starware.DatingApp.Core.ServiceContracts;
using Starware.DatingApp.Infrastructure;
using Starware.DatingApp.Persistence;
using Starware.DatingApp.Persistence.Repositories;

namespace Starware.DatingApp.API.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static void AddApplicationServices(this IServiceCollection services ,IConfiguration configuration)
        {
            services.AddDbContext<DatingAppContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IUserService, UserService>();

           services.AddScoped<ITokenService, TokenService>();
        }
    }
}
