using Starware.DatingApp.Core.Domains;
using Starware.DatingApp.Core.DTOs;
using Starware.DatingApp.Core.ServiceContracts;
using Starware.DatingApp.SharedKernal.Common;
using Starware.DatingApp.SharedKernal.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserService userService;

        public AccountService(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<ApiResponse<UserDto>> Login(LoginDto loginData)
        {
            var response = new ApiResponse<UserDto>();

            var user = await this.userService.GetByUsername(loginData.UserName);

            if (user == null)
            {
                response.Message = "Username is not Exist !!";
                return response;
            }

            var hmac = new HMACSHA512(user.PasswordSalt);

            var userLoginPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginData.Password));

            if (userLoginPassword.Length == user.PasswordHash.Length)
            {
                for (int i = 0; i < userLoginPassword.Length; i++)
                {
                    if (user.PasswordHash[i] != userLoginPassword[i])
                    {
                        response.Message = "Password is inccorect !!";
                        response.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                        return response;
                    }
                }
            }else
            {
                response.Message = "Password is inccorect !!";
                response.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                return response;
            }
            response.Data = new UserDto();
            response.Data.Age = user.BirthDate.GetAgeFromDate();
            response.Data.Name = $"{user.FirstName } {user.MiddleName} {user.LastName}";
            return response;
        }

        public async Task<ApiResponse<int>> Register(RegisterDto registerData)
        {
            var response = new ApiResponse<int>();
            var hmac = new HMACSHA512();
            var user = new AppUser()
            {
                UserName = registerData.UserName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerData.Password)),
                PasswordSalt = hmac.Key,
                FirstName = registerData.FirstName,
                MiddleName = registerData.MiddleName,
                LastName = registerData.LastName,
                BirthDate = registerData.BirthDate
            };

            var newUserId = await this.userService.AddUser(user);
            response.Data = newUserId;

            return response;
        }
    }
}
