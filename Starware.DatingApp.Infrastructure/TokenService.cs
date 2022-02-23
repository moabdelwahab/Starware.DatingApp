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
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
        }

        public string CreateToken(AppUser appUser)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.NameId , appUser.UserName),
                new Claim(JwtRegisteredClaimNames.Birthdate , appUser.BirthDate.ToString()),
                new Claim(JwtRegisteredClaimNames.Name , appUser.FirstName +' '+ appUser.MiddleName + ' '+ appUser.LastName ),
                new Claim(JwtRegisteredClaimNames.Email,"Mohamed.abdelwhab@Linkdev.com" ),
                new Claim(JwtRegisteredClaimNames.Gender, appUser.Gender),

            };

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
