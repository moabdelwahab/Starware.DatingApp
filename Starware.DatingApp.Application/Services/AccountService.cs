using Microsoft.AspNetCore.Identity;
using Starware.DatingApp.Core.Domains;
using Starware.DatingApp.Core.DTOs.Users;
using Starware.DatingApp.Core.InfrastructureContracts;
using Starware.DatingApp.Core.ServiceContracts;
using Starware.DatingApp.SharedKernal.Common;
using Starware.DatingApp.SharedKernal.Utilities;
using System.Security.Cryptography;

namespace Starware.DatingApp.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserService userService;
        private readonly ITokenService tokenService;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountService(IUserService userService,
                              ITokenService tokenService,
                              UserManager<AppUser> userManager,
                              SignInManager<AppUser> signInManager
            )
        {
            this.userService = userService;
            this.tokenService = tokenService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        public async Task<ApiResponse<UserDto>> Login(LoginDto loginData)
        {
            var response = new ApiResponse<UserDto>();

            var getUserResponse = await this.userService.GetByUsername(loginData.UserName);

            if (getUserResponse.Data == null)
            {
                response.Message = "Username is not Exist !!";
                response.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                return response;
            }

            //var hmac = new HMACSHA512(user.PasswordSalt);

            //var userLoginPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginData.Password));

            //if (userLoginPassword.Length == user.PasswordHash.Length)
            //{
            //    for (int i = 0; i < userLoginPassword.Length; i++)
            //    {
            //        if (user.PasswordHash[i] != userLoginPassword[i])
            //        {
            //            response.Message = "Password is inccorect !!";
            //            response.StatusCode = System.Net.HttpStatusCode.Unauthorized;
            //            return response;
            //        }
            //    }
            //}
            //else
            //{
            //    response.Message = "Password is inccorect !!";
            //    response.StatusCode = System.Net.HttpStatusCode.Unauthorized;
            //    return response;
            //}

            var user = getUserResponse.Data;
            var signIn = await signInManager.CheckPasswordSignInAsync(user, loginData.Password, false);

            if (!signIn.Succeeded)
            {
                response.Data = null;
                response.Message = "invalid Password";
                response.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                return response;
            }

            response.Data = new UserDto();
            response.Data.UserName = user.UserName;
            response.Data.Age = user.BirthDate.GetAgeFromDate();
            response.Data.Name = $"{user.FirstName } {user.MiddleName} {user.LastName}";
            response.Data.PhotoUrl = user?.Photos?.FirstOrDefault(x => x.IsMain)?.Url;
            response.Data.Token =await  this.tokenService.CreateToken(user);
            return response;
        }

        public async Task<ApiResponse<UserDto>> Register(RegisterDto registerData)
        {
            var response = new ApiResponse<UserDto>();
            var hmac = new HMACSHA512();
            var user = new AppUser()
            {
                UserName = registerData.UserName,
                //PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerData.Password)),
                //PasswordSalt = hmac.Key,
                FirstName = registerData.FirstName,
                MiddleName = registerData.MiddleName,
                LastName = registerData.LastName,
                BirthDate = registerData.BirthDate
            };

            var newUserId = await userManager.CreateAsync(user);

            var userDto = new UserDto()
            {
                UserName = registerData.UserName,
                Age = registerData.BirthDate.GetAgeFromDate(),
                Token =await this.tokenService.CreateToken(user),
                Name = $"{user.FirstName} {user.MiddleName} {user.LastName}"
            };
            response.Data = userDto;

            return response;
        }
    }
}
