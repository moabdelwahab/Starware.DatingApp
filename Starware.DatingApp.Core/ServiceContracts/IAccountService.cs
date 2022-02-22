using Starware.DatingApp.Core.Domains;
using Starware.DatingApp.Core.DTOs.Users;
using Starware.DatingApp.SharedKernal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Core.ServiceContracts
{
    public interface IAccountService
    {
        Task<ApiResponse<UserDto>> Login(LoginDto loginData);
        Task<ApiResponse<UserDto>> Register(RegisterDto registerData);
    }
}
