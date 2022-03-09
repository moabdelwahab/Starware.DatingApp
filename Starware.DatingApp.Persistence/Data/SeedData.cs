using Microsoft.AspNetCore.Identity;
using Starware.DatingApp.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Starware.DatingApp.Persistence.Data
{
    public class SeedData
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager)
        {
            var userData = await File.ReadAllTextAsync("Data/UsersSeeds.json");

            if (!string.IsNullOrEmpty(userData))
            {
                List<AppUser> appUsers = JsonSerializer.Deserialize<List<AppUser>>(userData);


                List<AppRole> roles = new List<AppRole>()
                {
                    new AppRole(){Name ="Admin"},
                    new AppRole(){Name ="Moderate"},
                    new AppRole(){Name ="Member"},
                };
                foreach (AppRole role in roles)
                {
                    await roleManager.CreateAsync(role);
                }
                foreach (AppUser user in appUsers)
                {
                    //using var hmac = new HMACSHA512();
                    //user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("123456"));
                    //user.PasswordSalt = hmac.Key;

                    user.UserName = user.FirstName.ToLower();
                    await userManager.CreateAsync(user, "P@$$sw0rd");
                    await userManager.AddToRoleAsync(user, "Member");
                }

                AppUser admin = new AppUser()
                {
                    UserName = "Admin"
                };
                await userManager.CreateAsync(admin, "P@$$sw0rd");

                await userManager.AddToRoleAsync(admin, "Admin");
            }

        }
    }
}
