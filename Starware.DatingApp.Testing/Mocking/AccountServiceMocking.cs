using Starware.DatingApp.Core.DTOs.Users;
using Starware.DatingApp.Core.ServiceContracts;
using Starware.DatingApp.SharedKernal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Testing.Mocking
{
    internal class AccountServiceMocking : IAccountService
    {
        public Task<ApiResponse<UserDto>> Login(LoginDto loginData)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<UserDto>> Register(RegisterDto registerData)
        {
            throw new NotImplementedException();
        }
    }
}
