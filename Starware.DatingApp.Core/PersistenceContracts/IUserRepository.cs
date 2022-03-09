using Starware.DatingApp.Core.Domains;
using Starware.DatingApp.Core.DTOs.Users;
using Starware.DatingApp.Core.Requests;
using Starware.DatingApp.SharedKernal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Core.PersistenceContracts
{
    public interface IUserRepository 
    {
        Task<PagedList<MemberDto>> GetUsersWithData(GetAllUsersRequest getAllUsersRequest);
        Task<AppUser> GetByUserName(string userName);
        Task<DateTime> LogUserActivity(string userName);
        Task<IEnumerable<AppUser>> GetAll();
        Task<AppUser> GetById(int id);
        Task<int> Update(AppUser entity);
        Task<bool> Delete(AppUser entity);
        Task<int> Insert(AppUser entity);

    }
}
