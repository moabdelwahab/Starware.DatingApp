using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Starware.DatingApp.Persistence;
using Starware.DatingApp.Persistence.Data;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                DatingAppContext context = services.GetRequiredService<DatingAppContext>();
                
                if(!context.Users.Any())
                {
                   await SeedData.SeedUsers(context);
                }
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "an error occured while seeding data ");
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults( webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
