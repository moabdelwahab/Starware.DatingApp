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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<AppUser> GetAllUser()
        {
            return unitOfWork.UserRepository.GetAll();
        }
    }
}
