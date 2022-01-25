using Starware.DatingApp.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Core.ServiceContracts
{
    public interface IUserService
    {
        IEnumerable<AppUser> GetAllUser();
    }
}
