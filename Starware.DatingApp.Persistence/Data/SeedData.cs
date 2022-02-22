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
        public static async Task SeedUsers(DatingAppContext datingAppContext)
        {
            var userData = await File.ReadAllTextAsync("Data/UsersSeeds.json");
            
            if (!string.IsNullOrEmpty(userData))
            {
                List<AppUser> appUsers = JsonSerializer.Deserialize<List<AppUser>>(userData);

                foreach (AppUser user in appUsers)
                {
                    using var hmac = new HMACSHA512();
                    user.UserName = user.FirstName.ToLower();
                    user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("123456"));
                    user.PasswordSalt = hmac.Key;
                    datingAppContext.Users.Add(user);
                }
                await datingAppContext.SaveChangesAsync();
            }
            
        }
    }
}
