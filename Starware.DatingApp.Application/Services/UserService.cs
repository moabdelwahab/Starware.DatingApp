using Starware.DatingApp.Core.Domains;
using Starware.DatingApp.Core.PersistenceContracts;
using Starware.DatingApp.Core.ServiceContracts;
using Starware.DatingApp.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Application.Services
{
    internal class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserService()
        {

        }
        public IEnumerable<AppUser> GetAllUser()
        {
           using(DatingAppContext context = new DatingAppContext())
            {

            }
        }
    }
}
