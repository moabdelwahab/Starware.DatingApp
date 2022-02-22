using Starware.DatingApp.Core.Domains;
using Starware.DatingApp.Core.DTOs;
using Starware.DatingApp.Core.DTOs.Users;
using Starware.DatingApp.SharedKernal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Core.ServiceContracts
{
    public interface IUserService
    {
        Task<ApiResponse<IEnumerable<MemberDto>>> GetAllUser();
        Task<ApiResponse<AppUser>> GetById(int id);
        Task<ApiResponse<int>> AddUser(AppUser user);
        Task<ApiResponse<MemberDto>> GetMemberByUsername(string username);
        Task<ApiResponse<AppUser>> GetByUsername(string username);
        Task<ApiResponse<bool>> UpdateUser(MemberDto user);
    }
}
