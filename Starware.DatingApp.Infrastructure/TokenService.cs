using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Starware.DatingApp.Core.Domains;
using Starware.DatingApp.Core.InfrastructureContracts;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Starware.DatingApp.Infrastructure     
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> userManager;
        private readonly SymmetricSecurityKey _key;


        public TokenService(IConfiguration configuration,UserManager<AppUser> userManager)
        {
            this._configuration = configuration;
            this.userManager = userManager;
            this._key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
        }

        public async Task<string> CreateToken(AppUser appUser)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.NameId , appUser.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName , appUser.UserName.ToString()),

            };

            var roles = await userManager.GetRolesAsync(appUser);

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = creds,
                Expires = DateTime.Now.AddHours(5),
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var token = tokenHandler.CreateToken(tokenDesc);

            return tokenHandler.WriteToken(token);

        }
    }
}
